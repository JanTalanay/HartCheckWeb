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
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;

        private readonly IMapper _mapper;

        public DoctorScheduleController(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }
        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<DoctorScheduleDto>))]
        public IActionResult GetPatients()
        {
            var bug = _mapper.Map<List<DoctorScheduleDto>>(_doctorScheduleRepository.GetDoctorSchedules());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("doctor/{doctorID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(DoctorSchedule))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsID(int doctorID)
        {
            if (!_doctorScheduleRepository.DoctorScheduleExist(doctorID))
            {
                return NotFound();
            }
            var user = _mapper.Map<DoctorScheduleDto>(_doctorScheduleRepository.GetDoctorSchedule(doctorID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] DoctorScheduleDto doctorSchedCreate)
        {
            if (doctorSchedCreate == null)
            {
                return BadRequest(ModelState);
            }
            var patient = _doctorScheduleRepository.GetDoctorSchedules();
            //.Where(e => e.patientDoctorID == patientCreate.usersID)
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
            var doctorSchedMap = _mapper.Map<DoctorSchedule>(doctorSchedCreate);

            try
            {
                _doctorScheduleRepository.CreateDoctorSchedule(doctorSchedMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(doctorSchedMap);
        }
        [HttpGet("patient/{patientID}")]
        [ProducesResponseType(200, Type = typeof(List<DoctorDetailsAndScheduleDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctorsDetailsAndSchedules(int patientID)
        {
            if (!_doctorScheduleRepository.PatientDoctorExist(patientID))
            {
                return NotFound();
            }

            var doctorsDetailsAndSchedules = await _doctorScheduleRepository.GetDoctorsDetailsAndSchedules(patientID);

            var doctorsDetailsAndSchedulesDto = doctorsDetailsAndSchedules.Select(dds => new DoctorDetailsAndScheduleDto
            {
                Doctor = _mapper.Map<HealthCareProfessionalDto>(dds.Item1),
                DoctorName = _mapper.Map<HealthCareProfessionalName>(dds.Item2),
                DoctorSchedule = _mapper.Map<List<DateTime>>(dds.Item3)
            }).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(doctorsDetailsAndSchedulesDto);
        }
        [HttpGet("patient/{patientID}/schedules")]
        [ProducesResponseType(200, Type = typeof(Dictionary<int, List<DateTime>>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctorSchedulesForPatient(int patientID)
        {
            var datesByDoctor = await _doctorScheduleRepository.GetDoctorSchedulesForPatient(patientID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(datesByDoctor);
        }

    }
}
