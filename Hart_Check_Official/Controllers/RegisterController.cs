using AutoMapper;
using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Numerics;
using System.Net.Mail;
using System.Net;

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
        public IActionResult GetUsers()
        {
            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUser());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet("{userID}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByID(int userID)
        {
            if(!_userRepository.UserExists(userID))
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDto>(_userRepository.GetUsers(userID));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }
        [HttpGet("GetUsersByEmail/{email}")]//getting the users by ID
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByEmail(String email)
        {
            if (!_userRepository.UserExistsEmail(email))
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDto>(_userRepository.GetUsersEmail(email));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user); ;
        }

        //[HttpPost]//register
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateUser([FromBody] UserDto userCreate)
        //{
        //    if (userCreate == null)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var users = _userRepository.GetUser()
        //        .Where(e => e.email.Trim().ToUpper() == userCreate.password.TrimEnd().ToUpper())
        //        .FirstOrDefault();

        //    if (users != null)
        //    {
        //        ModelState.AddModelError("", "Already Exist");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var userMap = _mapper.Map<Users>(userCreate);

        //    if (!_userRepository.CreateUsers(userMap))
        //    {
        //        ModelState.AddModelError("", "Something Went Wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }

        //    // Generate a 4 digit OTP
        //    var otp = new Random().Next(1000, 9999).ToString();
        //    var otpHash = ComputeHash(otp + userCreate.email);

        //    // SMTP client setup
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"),
        //        EnableSsl = true
        //    };

        //    // Create the email message
        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(userMap.email), // Sender's email
        //        Subject = "Your OTP",
        //        Body = $"Your OTP is {otp}"
        //    };

        //    mailMessage.To.Add(userCreate.email); // Receiver's email
        //    smtpClient.Send(mailMessage);

        //    return Ok(otpHash);
        //}

        [HttpPost]//register also adding what role
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userCreate)
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

            try
            {
                await _userRepository.CreateUsersAsync(userMap);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something Went Wrong while saving: " + ex.Message);
                return StatusCode(500, ModelState);
            }

            // Generate a 4 digit OTP
            var otp = new Random().Next(1000, 9999).ToString();

            // Compute the hash of the OTP and email
            var otpHash = ComputeHash(otp + userCreate.email);

            // SMTP client setup
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"),
                EnableSsl = true
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(userMap.email),
                Subject = "Your OTP",
                Body = $"Your OTP is {otp}"
            };

            mailMessage.To.Add(userCreate.email);
            smtpClient.Send(mailMessage);

            var response = new CreateUserResponse
            {
                UsersID = userMap.usersID,
                Email = userMap.email,
                Password = userMap.password,
                OTPHash = otpHash
            };

            return Ok(response);
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
        [HttpPut("{userID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userID, [FromBody] UserDto userUpdate)
        {
            if(userUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (userID != userUpdate.usersID)
            {
                ModelState.AddModelError("usersID", "The 'usersID' in the URL does not match the 'usersID' in the request body.");
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExists(userID))
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
        [HttpPost("ForgotPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            if (!_userRepository.UserExistsEmail(forgotPassword.Email))
            {
                return NotFound();
            }

            var user = _userRepository.GetUsersEmail(forgotPassword.Email);
            var otp = new Random().Next(1000, 9999).ToString(); // Generate a 4 digit OTP
            var otpHash = ComputeHash(otp + forgotPassword.Email);  // Compute the hash of the OTP and email

            var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
            {
                Port = 587, // Replace with your SMTP server's port
                Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"), // Replace with your SMTP server's username and password
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(user.email), // Replace with the sender's email
                Subject = "Your OTP",
                Body = $"Your OTP is {otp}"
            };

            mailMessage.To.Add(forgotPassword.Email);
            smtpClient.Send(mailMessage);

            //return Ok(new { Message = $"An OTP has been sent to {forgotPassword}", OtpHash = otpHash });
            return Ok(otpHash);
        }
        [HttpPost("VerifyOtp")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult VerifyOtp([FromBody] OTPverificationDto otpVerification)
        {
            var computedHash = ComputeHash(otpVerification.Otp + otpVerification.Email);
            if (computedHash == otpVerification.OtpHash)
            {
                return Ok("OTP is verified");
            }

            return BadRequest("Invalid OTP");
        }
        [HttpPost("ChangePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePassword)
        {
            var computedHash = ComputeHash(changePassword.Otp + changePassword.Email);
            if (computedHash != changePassword.OtpHash)
            {
                return BadRequest("Invalid OTP");
            }

            var user = _userRepository.GetUsersEmail(changePassword.Email);
            user.password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword); // Hash the new password
            _userRepository.UpdateUsers(user);

            return Ok("Password has been changed");
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

