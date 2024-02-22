using System.Security.Claims;
using HartCheck_Doctor_test.Data;
using HartCheck_Doctor_test.DTO;
using HartCheck_Doctor_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Doctor_test.Controllers;

[Authorize(Policy = "Doctor")]
public class PatientController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly datacontext _dbContext;

    public PatientController(ILogger<HomeController> logger,datacontext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        
    }
    
    [HttpGet]
    [Route("Patient/AddDiagnosis")]
    public IActionResult AddDiagnosis()
    {
        if (int.TryParse(Request.Query["patientID"], out int patientID))
        {
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            if (patId == null)
            {
                return NotFound();
            }
            var model = new DiagnosisDto()
            {
                patientID = patId.patientID
            };
            return View(model);
        }
        return BadRequest();
    }
        
    [HttpPost]
    [Route("Patient/AddDiagnosis")]
    public IActionResult AddDiagnosis(DiagnosisDto diagnosisDto)
    {
        Console.WriteLine(diagnosisDto.patientID);
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var doctorID = doctor.doctorID;
                var consultationID = _dbContext.Consultation
                    .Where(c => c.patientID == diagnosisDto.patientID) // Replace patientID with the actual patient ID you want to retrieve the consultationID for
                    .Join(_dbContext.PatientsDoctor,
                        c => c.patientID,
                        pd => pd.patientID,
                        (c, pd) => new { Consultation = c, PatientsDoctor = pd })
                    .Where(cp => cp.PatientsDoctor.doctorID == doctorID) // Replace doctorID with the actual doctor ID from the claims
                    .Select(cp => cp.Consultation.consultationID)
                    .FirstOrDefault();
                var diagnosis = new Diagnosis()
                {
                    consultationID = consultationID,
                    diagnosis = diagnosisDto.Diagnosis
                };
                _dbContext.Diagnosis.Add(diagnosis);
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        
        return View();
    }
        
    [HttpGet]
    [Route("Patient/AddCondition")]
    public IActionResult AddCondition()
    {
        if (int.TryParse(Request.Query["patientID"], out int patientID))
        {
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            if (patId == null)
            {
                return NotFound();
            }
            var model = new ConditionDto()
            {
                patientID = patId.patientID
            };
            return View(model);
        }
        return BadRequest();

    }
        
    [HttpPost]
    [Route("Patient/AddCondition")]
    public IActionResult AddCondition(ConditionDto conditionDto)
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var doctorID = doctor.doctorID;
                var consultationID = _dbContext.Consultation
                    .Where(c => c.patientID == conditionDto.patientID) // Replace patientID with the actual patient ID you want to retrieve the consultationID for
                    .Join(_dbContext.PatientsDoctor,
                        c => c.patientID,
                        pd => pd.patientID,
                        (c, pd) => new { Consultation = c, PatientsDoctor = pd })
                    .Where(cp => cp.PatientsDoctor.doctorID == doctorID) // Replace doctorID with the actual doctor ID from the claims
                    .Select(cp => cp.Consultation.consultationID)
                    .FirstOrDefault();
                foreach (var condition in conditionDto.Condition)
                {
                    var newCondition = new Condition()
                    {
                        consultationID = consultationID,
                        condition = condition
                    };
                    _dbContext.Condition.Add(newCondition);
                }
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        return View();
    }
    [HttpGet]
    [Route("Patient/AddMedicine")]
    public IActionResult AddMedicine()
    {
        if (int.TryParse(Request.Query["patientID"], out int patientID))
        {
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            if (patId == null)
            {
                return NotFound();
            }
            var model = new MedicineDto()
            {
                patientID = patId.patientID
            };
            return View(model);
        }
        return BadRequest();
    }
        
    [HttpPost]
    [Route("Patient/AddMedicine")]//to be revised
    public IActionResult AddMedicine(MedicineDto medicineDto)
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var doctorID = doctor.doctorID;
                var consultationID = _dbContext.Consultation
                    .Where(c => c.patientID == medicineDto.patientID) // Replace patientID with the actual patient ID you want to retrieve the consultationID for
                    .Join(_dbContext.PatientsDoctor,
                        c => c.patientID,
                        pd => pd.patientID,
                        (c, pd) => new { Consultation = c, PatientsDoctor = pd })
                    .Where(cp => cp.PatientsDoctor.doctorID == doctorID) // Replace doctorID with the actual doctor ID from the claims
                    .Select(cp => cp.Consultation.consultationID)
                    .FirstOrDefault();
                var medicine = new Medicine()
                {
                    consultationID = consultationID,
                    medicine = medicineDto.Medicine
                };
                _dbContext.Medicine.Add(medicine);
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        
        return View();
    }

    [HttpGet]
    public IActionResult BpThreshold()
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var doctorID = doctor.doctorID;
                var viewModel = new BpThresholdViewModel();
                viewModel.Patients = _dbContext.PatientsDoctor
                    .Where(pd => pd.doctorID == doctorID)
                    .Select(pd => new SelectListItem
                    {
                        Value = pd.patientID.ToString(),
                        Text = pd.patient.User.firstName +" "+ pd.patient.User.lastName // replace Patient.Name with the actual patient name
                    });
                return View(viewModel);
            }
        }
        return View();
    }
    
    [HttpPost]
    public IActionResult BpThreshold(BpThresholdViewModel bpThresholdView)
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var bpThreshold = new BloodPressureThreshold()
                {
                    patientID = bpThresholdView.SelectedPatientId,
                    doctorID = doctor.doctorID,
                    systolicLevel = bpThresholdView.SystolicLevel,
                    diastolicLevel = bpThresholdView.DiastolicLevel,
                };
                _dbContext.BloodPressureThreshold.Add(bpThreshold);
                _dbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        return View();
    }
    public IActionResult PatientBP()
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var patients = _dbContext.PatientsDoctor
                    .Where(pd => pd.doctorID == doctor.doctorID)
                    .Join(_dbContext.Patients,
                        pd => pd.patientID,
                        p => p.patientID,
                        (pd, p) => new { PatientsDoctor = pd, Patient = p })
                    .Join(_dbContext.Users,
                        pdp => pdp.Patient.usersID,
                        u => u.usersID,
                        (pdp, u) => new BpViewModel()
                        {
                            PatientID = pdp.Patient.patientID,
                            FirstName = u.firstName,
                            LastName = u.lastName,
                        }).ToList();

                return View(patients);
            }
        }

        return View();

        /*return Json(new {});*/
    }
    [HttpGet]
    [Route("Patient/GetPatientBP/{patientId:int}")]
    public IActionResult GetPatientBP(int patientId)
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var isAssociated = _dbContext.PatientsDoctor
                    .Any(pd => pd.doctorID == doctor.doctorID && pd.patientID == patientId);
                if (isAssociated)
                {
                    var bloodPressureData = _dbContext.BloodPressure
                        .Where(bp => bp.patientID == patientId)
                        .Select(bp => new { bp.dateTaken, bp.systolic, bp.diastolic })
                        .ToList();
           
                    return Json(bloodPressureData);
                }
            }
        }
        return Json(null);
    }
    
    
    [HttpGet]
    [Route("Patient/ViewPatient")]//to be fixed link
    public IActionResult ViewPatient()
    {

        if (int.TryParse(Request.Query["patientID"], out int patientID))
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            if (patId == null)
            {
                return NotFound();
            }

            Console.WriteLine(patId.patientID);

            if (int.TryParse(userID, out int userIDInt))
            {
                var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(u => u.usersID == userIDInt);
                Console.WriteLine(doctor.doctorID);
                if (doctor != null)
                {
                    var isAssociated = _dbContext.PatientsDoctor
                        .Any(pd => pd.doctorID == doctor.doctorID && pd.patientID == patientID);
                    if (isAssociated)
                    {
                        var patientsDoctors = _dbContext.Patients
                        .Where(p => p.patientID == patId.patientID)
                        .Join(_dbContext.Users,
                            p => p.usersID,
                            u => u.usersID,
                            (p, u) => new { Patient = p, User = u })
                        .Join(_dbContext.PreviousMedication,
                            pu => pu.Patient.patientID,
                            pm => pm.patientID,
                            (pu, pm) => new { PatientUser = pu, PreviousMedication = pm })
                        .Join(_dbContext.MedicalCondition,
                            pum => pum.PatientUser.Patient.patientID,
                            mc => mc.patientID,
                            (pum, mc) => new
                            {
                                PatientUser = pum.PatientUser, PreviousMedication = pum.PreviousMedication,
                                MedicalCondition = mc
                            })
                        .Join(_dbContext.MedicalHistory,
                            pumc => pumc.PatientUser.Patient.patientID,
                            mh => mh.patientID,
                            (pumc, mh) => new PatientDoctorViewModel
                            {
                                PatientID = pumc.PatientUser.Patient.patientID,
                                FirstName = pumc.PatientUser.User.firstName,
                                LastName = pumc.PatientUser.User.lastName,
                                Email = pumc.PatientUser.User.email,
                                Phone = pumc.PatientUser.User.phoneNumber,
                                CurrentCondition = pumc.MedicalCondition.medicalCondition,
                                PreviousCondition = pumc.MedicalCondition.conditionName,
                                PreviousMedications = pumc.PreviousMedication.previousMed,
                                SurgicalHistory = mh.pastSurgicalHistory
                            })
                        .ToList();

                    return View(patientsDoctors);
                    }
                    
                }
            }

            return View();
        }
        return View();
    }

    [HttpGet]
    public IActionResult ViewConsultation()
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
        //var doctorID = _dbContext.Users.FirstOrDefault(u => u.usersID == userID );
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(u => u.usersID == userIDInt);
            if (doctor != null)
            {
                var consultation = _dbContext.Consultation
                    .Join(_dbContext.DoctorSchedule,
                        c => c.doctorSchedID,
                        ds => ds.doctorSchedID,
                        (c, ds) => new { Consultation = c, DoctorSchedule = ds })
                    .Join(_dbContext.PatientsDoctor,
                        cds => cds.Consultation.patientID,
                        pd => pd.patientID,
                        (cds, pd) => new { ConsultationDoctorSchedule = cds, PatientsDoctor = pd })
                    .Where(cds => cds.ConsultationDoctorSchedule.DoctorSchedule.doctorID == doctor.doctorID)
                    .Join(_dbContext.Patients,
                        cdsd => cdsd.PatientsDoctor.patientID,
                        p => p.patientID,
                        (cdsd, p) => new { ConsultationDoctorSchedulePatientsDoctor = cdsd, Patient = p })
                    .Join(_dbContext.Users,
                        cdsp => cdsp.Patient.usersID,
                        u => u.usersID,
                        (cdsp, u) => new ConsultationViewModel()
                        {
                            consultationID = cdsp.ConsultationDoctorSchedulePatientsDoctor.ConsultationDoctorSchedule.Consultation.consultationID,
                            FirstName = u.firstName,
                            LastName = u.lastName,
                            Email = u.email,
                            Phone = u.phoneNumber,
                            schedDateTime = cdsp.ConsultationDoctorSchedulePatientsDoctor.ConsultationDoctorSchedule.DoctorSchedule.schedDateTime
                        })
                    .ToList();
                return View(consultation);
                
            }

            return View();
        }
        return View();
    }

    [HttpPost]
    public IActionResult ViewConsultation(int consultationId)
    {
        DeleteConsultation(consultationId);
        return View();
    }
    public void DeleteConsultation(int consultationId)
    {
        var consultation = _dbContext.Consultation
            .FirstOrDefault(c => c.consultationID == consultationId);

        if (consultation != null)
        {
            _dbContext.Consultation.Remove(consultation);
            _dbContext.SaveChanges();
        }
    }


    

    [HttpGet]
    public IActionResult Chat()
    {
        return View();
    }
    

}