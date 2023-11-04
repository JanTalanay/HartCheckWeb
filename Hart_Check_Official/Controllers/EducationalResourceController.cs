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
    public class EducationalResourceController : ControllerBase
    {
        private readonly IEducationalResourceRepository _educationalResourceRepository;

        private readonly IMapper _mapper;

        public EducationalResourceController(IEducationalResourceRepository educationalResourceRepository, IMapper mapper)
        {
            _educationalResourceRepository = educationalResourceRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<EducationalResource>))]
        public IActionResult GetBugReport()
        {
            var educResource = _mapper.Map<List<EducationalResourceDto>>(_educationalResourceRepository.GetEducationalResource());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(educResource);
        }
        [HttpGet("{resourceID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(EducationalResource))]
        [ProducesResponseType(400)]
        public IActionResult GetBugReportID(int resourceID)
        {
            if (!_educationalResourceRepository.EducationalResourceExist(resourceID))
            {
                return NotFound();
            }
            var educResource = _mapper.Map<EducationalResourceDto>(_educationalResourceRepository.GetEducationalResources(resourceID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(educResource); ;
        }
    }
}
