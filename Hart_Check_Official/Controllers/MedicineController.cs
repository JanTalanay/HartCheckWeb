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
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;

        private readonly IMapper _mapper;
        public MedicineController(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicineDto>))]
        public IActionResult GetMedicine()
        {
            var meds = _mapper.Map<List<MedicineDto>>(_medicineRepository.GetMedicines());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(meds);
        }
        [HttpGet("{patientID}/{doctorID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicineDto>))]
        public IActionResult GetMedicinesByPatientId(int patientID, int doctorID)
        {
            var medicines = _mapper.Map<List<MedicineDto>>(_medicineRepository.GetMedicinesByPatientID(patientID, doctorID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(medicines);
        }
    }
}
