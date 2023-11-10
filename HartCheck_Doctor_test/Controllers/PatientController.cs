using System.Security.Claims;
using HartCheck_Doctor_test.Data;
using HartCheck_Doctor_test.DTO;
using HartCheck_Doctor_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Doctor_test.Controllers;

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
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
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
                var condition = new Condition()
                {
                    consultationID = consultationID,
                    condition = conditionDto.Condition
                };
                _dbContext.Condition.Add(condition);
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        return View();
    }
    [HttpGet]
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
    [Route("Patient/AddMedicine")]
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
    [Authorize(Policy = "Doctor")]
    [Route("Patient/ViewPatient")]
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

            return View();
        }
        return View();
    }

    [HttpGet]
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
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
    [Authorize(Policy = "Doctor")]
    public IActionResult Chat()
    {
        return View();
    }
    

}