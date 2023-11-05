using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsDoctorController : ControllerBase
    {
        private readonly IPatientsDoctorRepository _patientsDoctorRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public PatientsDoctorController(IPatientsDoctorRepository patientsDoctorRepository, IUserRepository userRepository, IMapper mapper)
        {
            _patientsDoctorRepository = patientsDoctorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDoctorDto>))]
        public IActionResult GetPatientesDoctor()
        {
            var patientsDoctor = _mapper.Map<List<PatientsDoctorDto>>(_patientsDoctorRepository.GetPatientsDoctors());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(patientsDoctor);
        }
        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(PatientsDoctor))]
        [ProducesResponseType(400)]
        public IActionResult getPatientesDoctorID(int patientID)
        {
            if (!_patientsDoctorRepository.PatientsDoctorExist(patientID))
            {
                return NotFound();
            }
            var patientsDoctor = _mapper.Map<List<PatientsDoctorDto>>(_patientsDoctorRepository.GetPatientsDoctor(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(patientsDoctor);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatientesDoctor([FromBody] PatientsDoctorDto patientDoctorCreate)
        {
            if (patientDoctorCreate == null)
            {
                return BadRequest(ModelState);
            }
            var patient = _patientsDoctorRepository.GetPatientsDoctors();
                //.Where(e => e.patientDoctorID == patientCreate.usersID)
                //.FirstOrDefault();

            if (patient != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var patientDoctorMap = _mapper.Map<PatientsDoctor>(patientDoctorCreate);

            try
            {
                _patientsDoctorRepository.CreatePatientsDoctor(patientDoctorMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(patientDoctorMap);
        }
        [HttpGet("{patientID}/healthcareprofessionals")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
        public IActionResult GetHealthCareProfessionals(int patientID)
        {
            var healthCareProfessionals = _patientsDoctorRepository.GetHealthCareProfessionals(patientID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(healthCareProfessionals);
        }
        [HttpGet("{patientID}/doctors")]
        public IActionResult GetDoctorsByPatientId(int patientID)
        {
            var doctors = _patientsDoctorRepository.GetDoctorsByPatientId(patientID);
            return Ok(doctors);
        }
        [HttpPost("RescheduleAppointment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult RescheduleAppointment([FromBody] ConsultationRescheduleDto rescheduleAppointment)
        {
            var patientDoctor = _patientsDoctorRepository.GetPatientsDoctorByEmailAndName(rescheduleAppointment.email, rescheduleAppointment.firstName, rescheduleAppointment.lastName);

            if (patientDoctor == null)
            {
                return NotFound();
            }

            var doctorEmail = patientDoctor.doctor.User.email;

            // Check if the email is valid
            //if (!IsValidEmail(doctorEmail))
            //{
            //    return BadRequest(new { Message = "Invalid email address." });
            //}

            var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
            {
                Port = 587, // Replace with your SMTP server's port
                Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"), // Replace with your SMTP server's username and password
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(patientDoctor.patient.User.email), // Replace with the sender's email
                Subject = "Appointment Reschedule Request",
                Body = $"The patient {rescheduleAppointment.email} is requesting to reschedule their appointment."
            };

            mailMessage.To.Add(doctorEmail);
            smtpClient.Send(mailMessage);

            return Ok(new { Message = $"A reschedule request has been sent to the doctor's email" });
        }
    }
}
