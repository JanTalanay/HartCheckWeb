using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private readonly IConditionRepository _conditionRepository;

        private readonly IMapper _mapper;

        public ConditionController(IConditionRepository conditionRepository, IMapper mapper)
        {
            _conditionRepository = conditionRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConditionDto>))]
        public IActionResult GetCondition()
        {
            var condition = _mapper.Map<List<ConditionDto>>(_conditionRepository.GetConditions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(condition);
        }
        [HttpGet("{patientID}/{doctorID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConditionDto>))]
        public IActionResult GetConditionsByPatientId(int patientID, int doctorID)
        {
            var conditions = _mapper.Map<List<ConditionDto>>(_conditionRepository.GetConditionsByPatientId(patientID, doctorID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(conditions);
        }

    }
}
