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
        public IActionResult GetBugReport()
        {
            var bug = _mapper.Map<List<BugReportDto>>(_bugRepository.GetBugReports());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bug);
        }
        [HttpGet("{bugID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(BugReport))]
        [ProducesResponseType(400)]
        public IActionResult GetBugReportID(int bugID)
        {
            if (!_bugRepository.BugExists(bugID))
            {
                return NotFound();
            }
            var user = _mapper.Map<BugReportDto>(_bugRepository.GetBugReport(bugID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBugReport([FromBody] BugReportDto bugreportCreate)
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

            try
            {
                _bugRepository.CreateBugReport(bugReportMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }
            return Ok(bugReportMap);
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
        [HttpPut("{bugID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBugReport(int bugID, [FromBody] BugReportDto updateBugReport)
        {
            if (updateBugReport == null)
            {
                return BadRequest(ModelState);
            }
            if (bugID != updateBugReport.bugID)
            {
                return BadRequest(ModelState);
            }
            if (!_bugRepository.BugExists(bugID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bugMap = _mapper.Map<BugReport>(updateBugReport);

            if (!_bugRepository.UpdateBugReport(bugMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
