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
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult LoginUsers([FromBody] Login usersLogin)
        {
            if (usersLogin == null)
            {
                return BadRequest(ModelState);
            }
            //var users = _userRepository.GetUser();

            ////if (users != null)
            ////{
            ////    ModelState.AddModelError("", "Already Exist");
            ////    return StatusCode(422, ModelState);
            ////}

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var userMap = _mapper.Map<Users>(usersLogin);

            //if (_userRepository.LoginUsers(userMap))
            //{
            //    ModelState.AddModelError("", "Something Went Wrong while logging in");
            //    return StatusCode(500, ModelState);
            //}
            UserDto user;
            try
            {
                user = _mapper.Map<UserDto>(_userRepository.LoginUsers(usersLogin));
            }
            catch (Exception)
            {
                return BadRequest("Invalid email or password.");
            }

            //return Ok(new { Message = "Successfully Logged In", User = user });
            return Ok(user);
        }
    }
}
