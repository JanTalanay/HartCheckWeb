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
    public class BMITypeController : ControllerBase
    {
        private readonly IBMITypeRepository _bMITypeRepository;

        private readonly IMapper _mapper;

        public BMITypeController(IBMITypeRepository bMITypeRepository, IMapper mapper)
        {
            _bMITypeRepository = bMITypeRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<BMIType>))]
        public IActionResult GetUsers()
        {
            var user = _mapper.Map<List<BMITypeDto>>(_bMITypeRepository.GetBMITypes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet("{BMITypeID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(BMIType))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByID(int BMITypeID)
        {
            if (!_bMITypeRepository.BMITypeExists(BMITypeID))
            {
                return NotFound();
            }
            var user = _mapper.Map<BMITypeDto>(_bMITypeRepository.GetBMI(BMITypeID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
    }
}
