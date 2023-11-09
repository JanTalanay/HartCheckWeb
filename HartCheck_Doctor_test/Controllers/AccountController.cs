using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using HartCheck_Doctor_test.Data;
using HartCheck_Doctor_test.DTO;
using HartCheck_Doctor_test.FileUploadService;
using HartCheck_Doctor_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Doctor_test.Controllers
{
    public class AccountController : Controller
    {
        private readonly datacontext _dbContext;
        private readonly IFileUploadService fileUploadService;
        private string filePath; 
        
        public AccountController(datacontext dbContext, IFileUploadService fileUploadService)
        {
            _dbContext = dbContext;
            this.fileUploadService = fileUploadService;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDto userDto)
        {
            
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(u => u.email == userDto.email))
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                    return View(userDto);
                }
                var user = new Users()
                {
                    email = userDto.email,
                    firstName = userDto.firstName,
                    lastName = userDto.lastName,
                    password = userDto.password,
                    birthdate = userDto.birthdate,
                    gender = userDto.gender,
                    phoneNumber = userDto.phoneNumber,
                    role = 2
                };
                user.password = BCrypt.Net.BCrypt.HashPassword(userDto.password);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(userDto);
        }

        [HttpGet]
        public IActionResult License()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> uploadLicense(DoctorLicenseDTO licenseDto,IFormFile licenseFile)
        {
            if (_dbContext.DoctorLicense.Any(u => u.licenseID == licenseDto.LicenseID))
            {
                ModelState.AddModelError("License", "License is already registered");
                return View("License");
            }
            if (licenseFile != null && licenseFile.Length > 0)
            {
                filePath = await fileUploadService.UploadFileAsync(licenseFile);
                var license = new DoctorLicense()
                {
                    licenseID = licenseDto.LicenseID,
                    status = 0,
                    fileName = licenseFile.Name,
                    externalPath = filePath
                };
                _dbContext.DoctorLicense.Add(license);//Identity Insert is no
                _dbContext.SaveChanges();
                return View("Login");
            }
            return View("License");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login usersLogin)
        {
       
            if (ModelState.IsValid)
            {
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.email == usersLogin.email);

                if (existingUser == null )
                {
                    ModelState.AddModelError("Email", "Invalid email");
                    return View("Login");
                }


                if (!BCrypt.Net.BCrypt.Verify(usersLogin.password, existingUser.password))
                {
                    ModelState.AddModelError("Password", "Invalid password");
                    return View("Login");
                }


                return RedirectToAction("Index", "Home");
            }

            return View("Login");


        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.email == forgotPassword.Email);
                if (existingUser == null)
                {
                    return BadRequest("User not found");
                }
                var otp = new Random().Next(1000, 9999).ToString(); // Generate a 4 digit OTP
                var otpHash = ComputeHash(otp + forgotPassword.Email);  // Compute the hash of the OTP and email
                Console.WriteLine(otpHash);
                var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
                {
                    Port = 587, // Replace with your SMTP server's port
                    Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"), // Replace with your SMTP server's username and password
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(forgotPassword.Email), // Replace with the sender's email
                    Subject = "Change Pass",
                    Body = $"Click <a href=\"https://localhost:7215/Account/ChangePassword?hash={otpHash}&email={forgotPassword.Email}\">here</a> to change your password."
                };
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(forgotPassword.Email);
                smtpClient.Send(mailMessage);
                
                return View(forgotPassword);
            }
            return View("Login");
        }
        

        [HttpGet]
        [Route("Account/ChangePassword")]
        public IActionResult ChangePassword(string hash, string email)
        {
            var otpHash = hash;
            var otp = hash.Substring(0, 4);
            var mail = hash.Substring(4);

            var computedHash = otp+mail;

            if (computedHash != otpHash)
            {
                return BadRequest("Invalid OTP");
            }

            var user = _dbContext.Users.FirstOrDefault(e => e.email == email);//err
            if (user == null )
            {
                return BadRequest("User not found");
            }
            
            var model = new ChangePasswordDto()
            {
                Email = email,
                EmailHash = mail,
                Otp = otp,
                OtpHash = otpHash
            };
            
            return View(model);
        }
        
        [HttpPost]
        [Route("Account/ChangePassword")]
        public IActionResult ChangePassword( ChangePasswordDto changePassword, string email)
        {
            
            var computedHash = changePassword.Otp + changePassword.EmailHash;

            if (computedHash != changePassword.OtpHash)
            {
                return BadRequest("Invalid OTP");
            }

            var user = _dbContext.Users.FirstOrDefault(e => e.email == changePassword.Email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            user.password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword); // Hash the new password
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return View("Login");
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

