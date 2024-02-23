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
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryRepository _medicalHistoryRepository;


        private readonly IMapper _mapper;

        public MedicalHistoryController(IMedicalHistoryRepository medicalHistoryRepository, IMapper mapper)
        {
            _medicalHistoryRepository = medicalHistoryRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult GetMedicalHistory()
        {
            var bug = _mapper.Map<List<MedicalHistoryDto>>(_medicalHistoryRepository.GetMedicalHistories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(MedicalHistory))]
        [ProducesResponseType(400)]
        public IActionResult GetMedCondID(int patientID)
        {
            if (!_medicalHistoryRepository.medHistoryExsistPatientID(patientID))
            {
                return NotFound();
            }
            var user = _mapper.Map<MedicalHistoryDto>(_medicalHistoryRepository.GetMedicalHistoryPatientID(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMedicalHistory([FromBody] MedicalHistoryDto medicalHistoryCreate)
        {
            if (medicalHistoryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var bugReport = _medicalHistoryRepository.GetMedicalHistories()
                .Where(e => e.medicalHistory.Trim().ToUpper() == medicalHistoryCreate.medicalHistory.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (bugReport != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medicalHistoryMap = _mapper.Map<MedicalHistory>(medicalHistoryCreate);

            //if (!_medicalHistoryRepository.CreatemedHistory(medicalHistoryMap))
            //{
            //    ModelState.AddModelError("", "Something Went Wrong while saving");
            //    return StatusCode(500, ModelState);
            //}
            //return Ok("Successfully created");
            try
            {
                _medicalHistoryRepository.CreatemedHistory(medicalHistoryMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(medicalHistoryMap);
        }

        [HttpDelete("{medicalHistoryID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMedicalHistory(int medicalHistoryID)
        {
            if (!_medicalHistoryRepository.medHistoryExsist(medicalHistoryID))
            {
                return NotFound();
            }
            var medicalHistoryToDelete = _medicalHistoryRepository.GetMedicalHistory(medicalHistoryID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_medicalHistoryRepository.DeletemedHistory(medicalHistoryToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
        [HttpPut("{medicalHistoryID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMedicalHistory(int medicalHistoryID, [FromBody] MedicalHistoryDto updateMedicalHistory)
        {
            if (updateMedicalHistory == null)
            {
                return BadRequest(ModelState);
            }
            if (medicalHistoryID != updateMedicalHistory.medicalHistoryID)
            {
                return BadRequest(ModelState);
            }
            if (!_medicalHistoryRepository.medHistoryExsist(medicalHistoryID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var medicalHistoryMap = _mapper.Map<MedicalHistory>(updateMedicalHistory);

            if (!_medicalHistoryRepository.UpdatemedHistory(medicalHistoryMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
