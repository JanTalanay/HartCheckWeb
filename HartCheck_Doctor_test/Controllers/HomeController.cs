using System.ComponentModel.DataAnnotations;
using System.Data;
using HartCheck_Doctor_test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using HartCheck_Doctor_test.Data;
using HartCheck_Doctor_test.DTO;
using HartCheck_Doctor_test.FileUploadService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Doctor_test.Controllers
{
    [Authorize(Policy = "Doctor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly datacontext _dbContext;
        private readonly IFileUploadService fileUploadService;
        private string filePath; 

        public HomeController(ILogger<HomeController> logger,datacontext dbContext, IFileUploadService fileUploadService)
        {
            _dbContext = dbContext;
            _logger = logger;
            this.fileUploadService = fileUploadService;
        }
        
        public IActionResult Index()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            //var doctorID = _dbContext.Users.FirstOrDefault(u => u.usersID == userID );
            if(int.TryParse(userID, out int userIDInt))
            {
                var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(u => u.usersID == userIDInt);
                if (doctor != null)
                {
                    //ICollection<PatientsDoctor> patientsDoctors = _dbContext.PatientsDoctor.ToList();
                    //List<PatientDoctorViewModel> viewModel = patientsDoctors.Select(pd => new PatientDoctorViewModel//get actual names

                    var patientsDoctors = _dbContext.PatientsDoctor
                        .Where(pd => pd.doctorID == doctor.doctorID)
                        .Join(_dbContext.Patients,
                            pd => pd.patientID,
                            p => p.patientID,
                            (pd, p) => new { PatientsDoctor = pd, Patient = p })
                        .Join(_dbContext.Users,
                            pdp => pdp.Patient.usersID,
                            u => u.usersID,
                            (pdp, u) => new PatientDoctorViewModel
                                {
                                    PatientID = pdp.Patient.patientID,
                                    FirstName = u.firstName,
                                    LastName = u.lastName,
                                    Email = u.email,
                                    Phone = u.phoneNumber
                                }).ToList();
                    
                    return View(patientsDoctors);
                }

                return View("License");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult License()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> uploadLicense(DoctorLicenseDTO licenseDto,IFormFile licenseFile)
        {
            
            if (_dbContext.DoctorLicense.Any(u => u.licenseID == licenseDto.LicenseID))
            {
                ModelState.AddModelError("License", "License is already registered");
                return View("License");
            }
            if (licenseFile != null && licenseFile.Length > 0)
            {
                filePath = await fileUploadService.UploadFileAsync(licenseFile);
                var license = new DoctorLicense()
                {
                    licenseID = licenseDto.LicenseID,
                    status = 0,
                    fileName = licenseFile.Name,
                    externalPath = filePath
                };
                
                _dbContext.DoctorLicense.Add(license);//Identity Insert is no
                _dbContext.SaveChanges();
                
                return View("RegisterHP");
            }
            return View("License");
        }
        

        [HttpGet]
        public IActionResult RegisterHP()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Policy = "Doctor")]
        public IActionResult RegisterHP(HealthCareProfessionalDto hpDto)
        {
            if (_dbContext.HealthCareProfessional.Any(u => u.usersID == hpDto.usersID))
            {
                ModelState.AddModelError("License", "This person is already registered");
                return View("RegisterHP");
            }
            var hpProf = new HealthCareProfessional()
            {
                usersID = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userIDInt) ? userIDInt : 0,
                clinic = hpDto.clinic,
                licenseID = hpDto.licenseID,//must get 
                verification = 0
            };
            _dbContext.HealthCareProfessional.Add(hpProf);
            _dbContext.SaveChanges();
            
            return View("RegisterClinic");
        }

        [HttpGet]
        public IActionResult RegisterClinic()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult RegisterClinic(string id)
        {
            if (id == null)
            {
                return View("Index");
            }
            var uID = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userIDInt) ? userIDInt : 0;
            var docID = _dbContext.HealthCareProfessional.FirstOrDefault(u => u.usersID == uID);
            if (docID != null)
            {
                var hcClinic = new HealthCareClinic()
                {
                    doctorID = docID.doctorID,
                    clinicID = int.TryParse(docID.clinic, out int clinicIDInt) ? clinicIDInt : 0
                };

                _dbContext.HealthCareClinic.Add(hcClinic);
                _dbContext.SaveChanges();
                return View("Index");
            }
            return View("Index");
        }
        
        [HttpGet]
        public IActionResult ScheduleAppointment()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ScheduleAppointment(DoctorScheduleDto scheduleDto)
        {
            int rowCount = _dbContext.DoctorSchedule.Count();
            int newId = rowCount + 1;
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userID, out int userIDInt))
            {
                var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(u => u.usersID == userIDInt);
                var sched = new DoctorSchedule()
                {
                    doctorSchedID = newId,
                    doctorID = doctor.doctorID,
                    schedDateTime = scheduleDto.schedDateTime
                };
                _dbContext.DoctorSchedule.Add(sched);
                _dbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            
            return View("Index");
        }
        [HttpGet]
        public IActionResult ReportABug()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ReportABug(BugReportDto bugReportDto)
        {
            if (ModelState.IsValid)
            {
                var uID = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userIDInt) ? userIDInt : 0;
                if (uID != null)
                {
                    var bug = new BugReport()
                    {
                        usersID = uID,
                        featureID = bugReportDto.featureID,
                        description = bugReportDto.description
                    };
                    _dbContext.BugReport.Add(bug);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Index","Home");
            }
            return View(bugReportDto);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}