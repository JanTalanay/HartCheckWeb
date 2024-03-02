using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
    private readonly HttpClient _httpClient;
    public PatientController(ILogger<HomeController> logger,datacontext dbContext, IHttpClientFactory httpClientFactory)
    {
        _dbContext = dbContext;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        
    }

    [HttpGet]
    [Route("Patient/Payment")]
    public IActionResult Payment()
    {
        if (int.TryParse(Request.Query["patientID"], out int patientID))
        {
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            var patEmail = _dbContext.Patients
                .Where(c => c.patientID == patId.patientID)
                .Join(_dbContext.Users,
                    p => p.usersID,
                    u => u.usersID,
                    (p, u) => u) // Select the User entity from the join
                .Select(u => u.email) // Assuming the property name is Email
                .FirstOrDefault();
            if (patId == null)
            {
                return NotFound();
            }
            var model = new PaymentDto()
            {
                patientID = patId.patientID,
                email = patEmail
            };
            return View(model);
        }
        return BadRequest();
    }

    [HttpPost]
    [Route("Patient/Payment")]
    public async Task<IActionResult> Payment(PaymentDto userInvoice)
    {
        string url = "https://pg-sandbox.paymaya.com/invoice/v2/invoices";
        Console.WriteLine(userInvoice.patientID);
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        
        if (int.TryParse(userID, out int userIDInt))
        {
            var doctor = _dbContext.HealthCareProfessional.FirstOrDefault(d => d.usersID == userIDInt);
            if (doctor != null)
            {
                var doctorID = doctor.doctorID;
                var consultationID = _dbContext.Consultation
                    .Where(c => c.patientID == userInvoice.patientID) // Filter by patient ID
                    .Join(_dbContext.PatientsDoctor,
                        c => c.patientID,
                        pd => pd.patientID,
                        (c, pd) => new { Consultation = c, PatientsDoctor = pd })
                    .Join(_dbContext.Patients,
                        cp => cp.PatientsDoctor.patientID,
                        p => p.patientID,
                        (cp, p) => new { cp.Consultation, cp.PatientsDoctor, Patient = p })
                    .Join(_dbContext.Users,
                        cp => cp.Patient.usersID,
                        u => u.usersID,
                        (cp, u) => new { cp.Consultation, cp.PatientsDoctor, cp.Patient, User = u })
                    .Where(cp => cp.User.usersID == doctorID) // Filter by doctor ID
                    .Select(cp => new PaymentDto()
                    {
                        email = cp.User.email
                    });
                var request = new RequestModel
                {
                    invoiceNumber = "INV0001",
                    type = "SINGLE",
                    totalAmount = new TotalAmountModel
                    {
                        value = 300,
                        currency = "PHP"
                    },
                    items = new List<ItemModel>
                    {
                        new ItemModel
                        {
                            name = "HartCheck Subscription",
                            quantity = "1",
                            totalAmount = new TotalAmountModel
                            {
                                value = 300,
                                currency = "PHP"
                            }
                        }
                    },
                    redirectUrl = new RedirectModel
                    {
                        success = "https://www.merchantsite.com/success",//change to payment success go back to mobile app
                        failure = "https://www.merchantsite.com/failure",//payment failed html
                        cancel = "https://www.merchantsite.com/cancel"//payment has been cancelled html
                    },
                    requestReferenceNumber = "1551191039",
                    metadata = new { }
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("sk-X8qolYjy62kIzEbr0QRK1h4b4KDVHaNcwMYk39jInSl")));

                var response = await _httpClient.PostAsync(url, content);
                var responseJson = await response.Content.ReadAsStringAsync();
                

                if (response.IsSuccessStatusCode)
                {
                    var user = userInvoice.email;
                    var invoiceUrl = JsonSerializer.Deserialize<ResponseModel>(responseJson).invoiceUrl;
                    //Email code here
                    var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
                    {
                        Port = 587, // Replace with your SMTP server's port
                        Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"), // Replace with your SMTP server's username and password
                        EnableSsl = true
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(user), // Replace with the sender's email
                        Subject = "Pending Payment - HartCheck",
                        Body = $"Click here to pay for the subscription: {invoiceUrl}"
                    };

                    mailMessage.To.Add(userInvoice.email);
                    smtpClient.Send(mailMessage);

                    return RedirectToAction("Index","Home");
                }
                        
                        
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        return View();
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
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patId = _dbContext.Patients.FirstOrDefault(p => p.patientID == patientID);
            if (patId == null)
            {
                return NotFound();
            }
            var model = new ConditionDto()
            {
                patientID = patId.patientID
            };
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
                        var consultationID = _dbContext.Consultation
                            .Where(c => c.patientID == patId.patientID) // Replace patientID with the actual patient ID you want to retrieve the consultationID for
                            .Join(_dbContext.PatientsDoctor,
                                c => c.patientID,
                                pd => pd.patientID,
                                (c, pd) => new { Consultation = c, PatientsDoctor = pd })
                            .Where(cp => cp.PatientsDoctor.doctorID == doctor.doctorID) // Replace doctorID with the actual doctor ID from the claims
                            .Select(cp => cp.Consultation.consultationID)
                            .FirstOrDefault();
                        
                        
                        var schedDateTime = _dbContext.Consultation
                            .Where(c => c.consultationID == consultationID) // Use the consultationID obtained
                            .Join(_dbContext.DoctorSchedule,
                                c => c.doctorSchedID, // Assuming doctorSchedID is the key to join Consultation and DoctorSchedule
                                ds => ds.doctorSchedID,
                                (c, ds) => new { Consultation = c, DoctorSchedule = ds })
                            .Select(result => result.DoctorSchedule.schedDateTime)
                            .FirstOrDefault();
                        
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
                                    PatientUser = pum.PatientUser,
                                    PreviousMedication = pum.PreviousMedication,
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
                            .Join(_dbContext.Consultation,
                                p => p.PatientID,
                                c => c.patientID,
                                (p, c) => new { PatientDoctorViewModel = p, Consultation = c })
                            .Join(_dbContext.Diagnosis,
                                pc => pc.Consultation.consultationID,
                                d => d.consultationID,
                                (pc, d) => new PatientDoctorViewModel
                                {
                                    PatientID = pc.PatientDoctorViewModel.PatientID,
                                    FirstName = pc.PatientDoctorViewModel.FirstName,
                                    LastName = pc.PatientDoctorViewModel.LastName,
                                    Email = pc.PatientDoctorViewModel.Email,
                                    Phone = pc.PatientDoctorViewModel.Phone,
                                    CurrentCondition = pc.PatientDoctorViewModel.CurrentCondition,
                                    PreviousCondition = pc.PatientDoctorViewModel.PreviousCondition,
                                    PreviousMedications = pc.PatientDoctorViewModel.PreviousMedications,
                                    SchedDateTime = schedDateTime,
                                    SurgicalHistory = pc.PatientDoctorViewModel.SurgicalHistory,
                                    DiagnosisID = d.diagnosisID,
                                    Diagnosis = d.diagnosis
                                })
                            .ToList();
                        // use viewbag ViewBag.model2 = model2.PreviousCondition;
                        ViewBag.PatientsDoctors = patientsDoctors;
                    return View(model);
                    }
                    
                }
            }
            
            
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
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ChatDoc()
    {
        return View();
    }
    //chat logic here (post) 

}