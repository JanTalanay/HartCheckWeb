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
    public class MedicalConditionController : ControllerBase
    {
        private readonly IMedicalConditionRepository _medicalConditionRepository;

        private readonly IMapper _mapper;

        public MedicalConditionController(IMedicalConditionRepository medicalConditionRepository, IMapper mapper)
        {
            _medicalConditionRepository = medicalConditionRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list of all the data
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalCondition>))]
        public IActionResult getMedicalCondition()
        {
            var user = _mapper.Map<List<MedicalConditionDto>>(_medicalConditionRepository.GetMedicalConditions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpGet("{medCondID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(MedicalCondition))]
        [ProducesResponseType(400)]
        public IActionResult GetMedCondID(int medCondID)
        {
            if (!_medicalConditionRepository.MedicalConditionExists(medCondID))
            {
                return NotFound();
            }
            var user = _mapper.Map<MedicalConditionDto>(_medicalConditionRepository.GetMedicalCondition(medCondID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
        [HttpDelete("{medCondID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMedCond(int medCondID)
        {
            if (!_medicalConditionRepository.MedicalConditionExists(medCondID))
            {
                return NotFound();
            }
            var medCondToDelete = _medicalConditionRepository.GetMedicalCondition(medCondID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_medicalConditionRepository.DeleteMedicalCondition(medCondToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
        [HttpPut("{medCondID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMedCond(int medCondID, [FromBody] MedicalConditionDto updateMedCond)
        {
            if (updateMedCond == null)
            {
                return BadRequest(ModelState);
            }
            if (medCondID != updateMedCond.medCondID)
            {
                return BadRequest(ModelState);
            }
            if (!_medicalConditionRepository.MedicalConditionExists(medCondID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userMap = _mapper.Map<MedicalCondition>(updateMedCond);

            if (!_medicalConditionRepository.UpdateMedicalCondition(userMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMedicalCond([FromBody] MedicalConditionDto medCondCreate)
        {
            if (medCondCreate == null)
            {
                return BadRequest(ModelState);
            }
            var medCond = _medicalConditionRepository.GetMedicalConditions()
                .Where(e => e.medicalCondition.Trim().ToUpper() == medCondCreate.conditionName.Trim().ToUpper())
                .FirstOrDefault();

            if (medCond != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medCondMap = _mapper.Map<MedicalCondition>(medCondCreate);

            if (!_medicalConditionRepository.CreateMedicalCondition(medCondMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
