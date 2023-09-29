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
    public class BugReportController : ControllerBase
    {
        private readonly IBugReportRepository _bugRepository;

        private readonly IMapper _mapper;

        public BugReportController(IBugReportRepository bugRepository, IMapper mapper)
        {
            _bugRepository = bugRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult getUsers()
        {
            var bug = _mapper.Map<List<BugReportDto>>(_bugRepository.GetBugReports());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBugReport([FromBody] BugReportDto bugreportCreate)
        {
            if(bugreportCreate == null)
            {
                return BadRequest(ModelState);
            }
            var bugReport = _bugRepository.GetBugReports()
                .Where(e => e.description.Trim().ToUpper() == bugreportCreate.description.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(bugReport != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bugReportMap = _mapper.Map<BugReport>(bugreportCreate);

            if (!_bugRepository.CreateBugReport(bugReportMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }


        [HttpPost("{userID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBugReportID([FromBody] BugReportDto bugreportCreate)
        {
            if (bugreportCreate == null)
            {
                return BadRequest(ModelState);
            }
            var bugReport = _bugRepository.GetBugReports()
                .Where(e => e.description.Trim().ToUpper() == bugreportCreate.description.TrimEnd().ToUpper())
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
            var bugReportMap = _mapper.Map<BugReport>(bugreportCreate);

            if (!_bugRepository.CreateBugReport(bugReportMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }


        [HttpDelete("{bugID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBugReport(int bugID)
        {
            if (!_bugRepository.BugExists(bugID))
            {
                return NotFound();
            }
            var bugToDelete = _bugRepository.GetBugReport(bugID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bugRepository.DeleteBugReport(bugToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
    }
}
