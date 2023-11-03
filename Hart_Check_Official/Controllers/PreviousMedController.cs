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
    public class PreviousMedController : ControllerBase
    {
        private readonly IPreviousMedRepository _previousMedRepository;

        private readonly IMapper _mapper;

        public PreviousMedController(IPreviousMedRepository previousMedRepository, IMapper mapper)
        {
            _previousMedRepository = previousMedRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult GetPrevMed()
        {
            var user = _mapper.Map<List<PreviousMedDto>>(_previousMedRepository.GetPreviousMedications());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(PreviousMedication))]
        [ProducesResponseType(400)]
        public IActionResult GetMedCondID(int patientID)
        {
            if (!_previousMedRepository.PrevMedExistsPatientID(patientID))
            {
                return NotFound();
            }
            var user = _mapper.Map<PreviousMedDto>(_previousMedRepository.GetPrevMedPatientID(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
        [HttpPost]//Insert
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePrevMed([FromBody] PreviousMedDto userPrevMed)
        {
            if (userPrevMed == null)
            {
                return BadRequest(ModelState);
            }
            var users = _previousMedRepository.GetPreviousMedications()
                .Where(e => e.previousMed.Trim().ToUpper() == userPrevMed.previousMed.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var prevMedMap = _mapper.Map<PreviousMedication>(userPrevMed);

            //if (!_previousMedRepository.CreatePrevMed(prevMedMap))
            //{
            //    ModelState.AddModelError("", "Something Went Wrong while saving");
            //    return StatusCode(500, ModelState);
            //}
            try
            {
                _previousMedRepository.CreatePrevMed(prevMedMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(prevMedMap);
        }
        [HttpDelete("{prevMedID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUsers(int prevMedID)
        {
            if (!_previousMedRepository.PrevMedExists(prevMedID))
            {
                return NotFound();
            }
            var prevMedToDelete = _previousMedRepository.GetPrevMed(prevMedID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_previousMedRepository.DeletePrevMed(prevMedToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
    }
}
