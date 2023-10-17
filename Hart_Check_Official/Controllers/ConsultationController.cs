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
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationRepository _consultationRepository;

        private readonly IMapper _mapper;

        public ConsultationController(IConsultationRepository consultationRepository, IMapper mapper)
        {
            _consultationRepository = consultationRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Consultation>))]
        public IActionResult GetPatients()
        {
            var bug = _mapper.Map<List<PatientDto>>(_consultationRepository.GetConsultations());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("{consultationID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(Consultation))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsID(int consultationID)
        {
            if (!_consultationRepository.consultationExists(consultationID))
            {
                return NotFound();
            }
            var user = _mapper.Map<PatientDto>(_consultationRepository.GetConsultation(consultationID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] ConsultationDto consultationCreate)
        {
            if (consultationCreate == null)
            {
                return BadRequest(ModelState);
            }
            var patient = _consultationRepository.GetConsultations();
                //.Where(e => e.usersID == patientCreate.usersID)
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
            var consultMap = _mapper.Map<Consultation>(consultationCreate);

            if (!_consultationRepository.CreateConsultation(consultMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
