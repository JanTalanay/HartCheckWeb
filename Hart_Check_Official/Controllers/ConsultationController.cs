using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Hart_Check_Official.Repository;
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
        public IActionResult GetConsultation()
        {
            var consultation = _mapper.Map<List<ConsultationDto>>(_consultationRepository.GetConsultations());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(consultation);
        }
        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(Consultation))]
        [ProducesResponseType(400)]
        public IActionResult GetConsultationID(int patientID)
        {
            if (!_consultationRepository.consultationExistsPatientsID(patientID))
            {
                return NotFound();
            }
            var consultation = _mapper.Map<ConsultationDto>(_consultationRepository.GetConsultationPatientsID(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(consultation);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateConsultation([FromBody] ConsultationDto consultationCreate)
        {
            if (consultationCreate == null)
            {
                return BadRequest(ModelState);
            }
            var consultation = _consultationRepository.GetConsultations()
            .Where(e => e.doctorSchedID == consultationCreate.doctorSchedID)
            .FirstOrDefault();

            if (consultation != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var consultMap = _mapper.Map<Consultation>(consultationCreate);

            //if (!_consultationRepository.CreateConsultation(consultMap))
            //{
            //    ModelState.AddModelError("", "Something Went Wrong while saving");
            //    return StatusCode(500, ModelState);
            //}
            //return Ok("Successfully created");
            try
            {
                _consultationRepository.CreateConsultation(consultMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(consultMap);
        }
        [HttpDelete("{consultationID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteConsultation(int consultationID)
        {
            if (!_consultationRepository.consultationExists(consultationID))
            {
                return NotFound();
            }
            var consultationToDelete = _consultationRepository.GetConsultation(consultationID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_consultationRepository.DeleteConsultation(consultationToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }

        [HttpGet("doctor/{doctorID}/name")]
        public IActionResult GetDoctorName(int doctorID)
        {
            var doctorUser = _consultationRepository.GetDoctorUserByDoctorId(doctorID);
            if (doctorUser == null)
            {
                return NotFound();
            }

            return Ok(new { firstName = doctorUser.firstName, lastName = doctorUser.lastName });
        }
        [HttpGet("{patientID}/dates")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DoctorScheduleDto>))]
        public IActionResult GetDatesForPatient(int patientID)
        {
            if (!_consultationRepository.consultationExistsPatientsID(patientID))
            {
                return NotFound();
            }
            var doctorSchedules = _consultationRepository.GetDoctorSchedulesForPatient(patientID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(doctorSchedules);
        }
    }
}
