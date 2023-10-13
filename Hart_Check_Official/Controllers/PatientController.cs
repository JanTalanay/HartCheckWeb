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
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patients>))]
        public IActionResult GetPatients()
        {
            var bug = _mapper.Map<List<PatientDto>>(_patientRepository.GetPatient());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("{usersID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(Patients))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsID(int usersID)
        {
            if (!_patientRepository.patientExist(usersID))
            {
                return NotFound();
            }
            var user = _mapper.Map<PatientDto>(_patientRepository.GetPatients(usersID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] PatientDto patientCreate)
        {
            if (patientCreate == null)
            {
                return BadRequest(ModelState);
            }
            var patient = _patientRepository.GetPatient()
                .Where(e => e.usersID == patientCreate.usersID)
                .FirstOrDefault();

            if (patient != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var patientMap = _mapper.Map<Patients>(patientCreate);

            if (!_patientRepository.Createpatient(patientMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
