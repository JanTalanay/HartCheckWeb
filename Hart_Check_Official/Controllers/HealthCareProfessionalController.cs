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
    public class HealthCareProfessionalController : ControllerBase
    {
        private readonly IHealthCareProfessionalRepository _healthCareProfessionalRepository;

        private readonly IMapper _mapper;

        public HealthCareProfessionalController(IHealthCareProfessionalRepository healthCareProfessionalRepository, IMapper mapper)
        {
            _healthCareProfessionalRepository = healthCareProfessionalRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<HealthCareProfessionalDto>))]
        public IActionResult GetPatients()
        {
            var bug = _mapper.Map<List<HealthCareProfessionalDto>>(_healthCareProfessionalRepository.GetHealthCareProfessionals());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("{doctorID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(HealthCareProfessional))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsID(int doctorID)
        {
            if (!_healthCareProfessionalRepository.HealthCareProfessionalExist(doctorID))
            {
                return NotFound();
            }
            var user = _mapper.Map<HealthCareProfessionalDto>(_healthCareProfessionalRepository.GetHealthCareProfessional(doctorID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] HealthCareProfessionalDto healthCareCreate)
        {
            if (healthCareCreate == null)
            {
                return BadRequest(ModelState);
            }
            var patient = _healthCareProfessionalRepository.GetHealthCareProfessionals();
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
            var healthCareMap = _mapper.Map<HealthCareProfessional>(healthCareCreate);

            try
            {
                _healthCareProfessionalRepository.CreateHealthCareProfessional(healthCareMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(healthCareMap);
        }
    }
}
