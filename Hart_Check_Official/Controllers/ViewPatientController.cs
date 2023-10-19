using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewPatientController : ControllerBase
    {
        private readonly IViewPatientListsRepository _viewPatientListsRepository;
        private readonly IMapper _mapper;



        public ViewPatientController(IViewPatientListsRepository viewPatientListsRepository, IMapper mapper)
        {
            _viewPatientListsRepository = viewPatientListsRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]

        public IActionResult getUsers()
        {
            var user = _mapper.Map<List<Users>>(_viewPatientListsRepository.GetUser());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet("{userID}")]// Getting user info by their ID
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]
        public IActionResult getUser(int userID)
        {
            if (!_viewPatientListsRepository.UserExists(userID))
            {
                return NotFound();
            }
            var user = _mapper.Map<Users>(_viewPatientListsRepository.GetUsers(userID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

    }
}
