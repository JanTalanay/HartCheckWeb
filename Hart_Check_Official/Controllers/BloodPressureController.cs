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
    public class BloodPressureController : ControllerBase
    {
        private readonly IBloodPressureRepository _bloodPressureRepository;

        private readonly IMapper _mapper;

        public BloodPressureController(IBloodPressureRepository bloodPressureRepository, IMapper mapper)
        {
            _bloodPressureRepository = bloodPressureRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the BodyMass table
        [ProducesResponseType(200, Type = typeof(IEnumerable<BloodPressure>))]
        public IActionResult GetGetBloodPressure()
        {
            var bloodPressure = _mapper.Map<List<BloodPressureDto>>(_bloodPressureRepository.GetBloodPressures());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bloodPressure);
        }
        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(BloodPressure))]
        [ProducesResponseType(400)]
        public IActionResult GetBloodPressureID(int patientID)
        {
            if (!_bloodPressureRepository.BloodPressureExistPatientID(patientID))
            {
                return NotFound();
            }
            var bloodPressure = _mapper.Map<List<BloodPressureDto>>(_bloodPressureRepository.GetBloodPressPatientID(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bloodPressure);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGetBloodPressure([FromBody] BloodPressureDto bloodPressureCreate)
        {
            if (bloodPressureCreate == null)
            {
                return BadRequest(ModelState);
            }
            var bloodPressure = _bloodPressureRepository.GetBloodPressures()
                .Where(e => e.dateTaken == bloodPressureCreate.dateTaken)
                .FirstOrDefault();

            if (bloodPressure != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bloodpressureMap = _mapper.Map<BloodPressure>(bloodPressureCreate);

            if (!_bloodPressureRepository.CreateBloodPressure(bloodpressureMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpDelete("{bloodPressureID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGetBloodPressure(int bloodPressureID)
        {
            if (!_bloodPressureRepository.BloodPressureExist(bloodPressureID))
            {
                return NotFound();
            }
            var bodyMassToDelete = _bloodPressureRepository.GetBloodPress(bloodPressureID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bloodPressureRepository.DeleteBloodPressure(bodyMassToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
        [HttpPut("{bloodPressureID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGetBloodPressure(int bloodPressureID, [FromBody] BloodPressureDto updateBloodPressure)
        {
            if (updateBloodPressure == null)
            {
                return BadRequest(ModelState);
            }
            if (bloodPressureID != updateBloodPressure.bloodPressureID)
            {
                return BadRequest(ModelState);
            }
            if (!_bloodPressureRepository.BloodPressureExist(bloodPressureID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bloodpressureMap = _mapper.Map<BloodPressure>(updateBloodPressure);

            if (!_bloodPressureRepository.UpdateBloodPressure(bloodpressureMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
