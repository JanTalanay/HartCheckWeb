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
    public class BloodPressureThresholdController : ControllerBase
    {
        private readonly IBloodPressureThresholdRepository _bloodPressureThresholdRepository;

        private readonly IMapper _mapper;

        public BloodPressureThresholdController(IBloodPressureThresholdRepository bloodPressureThresholdRepository, IMapper mapper)
        {
            _bloodPressureThresholdRepository = bloodPressureThresholdRepository;
            _mapper = mapper;
        }

        [HttpGet("{patientID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(BloodPressureThreshold))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsID(int patientID)
        {
            if (!_bloodPressureThresholdRepository.PatientExists(patientID))
            {
                return NotFound();
            }
            var bloodPressureThreshold = _mapper.Map<BloodPressureThresholdDto>(_bloodPressureThresholdRepository.GetBloodPressureThreshold(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bloodPressureThreshold);
        }
    }
}
