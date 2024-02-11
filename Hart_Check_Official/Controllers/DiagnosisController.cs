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
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisRepository _diagnosisRepository;

        private readonly IMapper _mapper;

        public DiagnosisController(IDiagnosisRepository diagnosisRepository, IMapper mapper)
        {
            _diagnosisRepository = diagnosisRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiagnosisDto>))]
        public IActionResult GetDiagnosis()
        {
            var diagnosis = _mapper.Map<List<DiagnosisDto>>(_diagnosisRepository.GetDiagnoses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(diagnosis);
        }
        [HttpGet("{patientID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiagnosisDto>))]
        public IActionResult GetDiagnosesByPatientId(int patientID)
        {
            var diagnoses = _mapper.Map<List<DiagnosisDto>>(_diagnosisRepository.GetDiagnosisByPatientId(patientID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(diagnoses);
        }
    }
}
