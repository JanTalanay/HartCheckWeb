using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsDoctorController : ControllerBase
    {
        private readonly IPatientsDoctorRepository _patientsDoctorRepository;

        private readonly IMapper _mapper;

        public PatientsDoctorController(IPatientsDoctorRepository patientsDoctorRepository, IMapper mapper)
        {
            _patientsDoctorRepository = patientsDoctorRepository;
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

    }
}
