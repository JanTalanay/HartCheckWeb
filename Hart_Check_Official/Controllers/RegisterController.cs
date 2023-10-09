using AutoMapper;
using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public RegisterController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the Users table
        [ProducesResponseType(200, Type = typeof(IEnumerable<Users>))]
        public IActionResult getUsers()
        {
            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUser());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpPost]//register
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }
            var users = _userRepository.GetUser()
                .Where(e => e.email.Trim().ToUpper() == userCreate.password.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userMap = _mapper.Map<Users>(userCreate);

            if (!_userRepository.CreateUsers(userMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpDelete("{userID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUsers(int userID)
        {
            if (!_userRepository.UserExists(userID))
            {
                return NotFound();
            }
            var usersToDelete = _userRepository.GetUsers(userID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.DeleteUser(usersToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
        [HttpPut("{usersID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int usersID, [FromBody] UserDto userUpdate)
        {
            if(userUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (usersID != userUpdate.usersID)
            {
                ModelState.AddModelError("usersID", "The 'usersID' in the URL does not match the 'usersID' in the request body.");
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExists(usersID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usersMap = _mapper.Map<Users>(userUpdate);

            if (!_userRepository.UpdateUsers(usersMap))
            {
                ModelState.AddModelError("", "Something Went Wrong when Updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

