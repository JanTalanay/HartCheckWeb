﻿using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Authorization;
using HartCheck_Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HartCheck_Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHCProfessionalRepository _hcprofessionalRepository;
        private readonly IDoctorLicenseRepository _doctorlicenseRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly ApplicationDbContext _context;
        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IHCProfessionalRepository hcprofessionalRepository, IDoctorLicenseRepository doctorlicenseRepository)
        {
            _userRepository = userRepository;
            _hcprofessionalRepository = hcprofessionalRepository;
            _httpcontextAccessor = httpContextAccessor;
            _doctorlicenseRepository = doctorlicenseRepository;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            return View(users);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            if (userdetails == null)
            {
                return View("Error");
            }
            else
            {
                _userRepository.Delete(userdetails);
                return RedirectToAction("Index");
            }

        }
        [Authorize]
        public async Task<IActionResult> View(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var hcp = await _hcprofessionalRepository.GetProfessionalByUserIdAsync(id);
            var license = await _doctorlicenseRepository.GetLicenseByUserIdAsync(id);
            ViewData["UserId"] = id;
            if (user == null || hcp == null) return View("Error");
            var userVM = new ViewDoctorViewModel
            {
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phoneNumber = user.phoneNumber,
                clinic = hcp.clinic,
                licenseID = hcp.licenseID,
                verification = hcp.verification,
                fileName = license.fileName,
                externalPath = license.externalPath
            };
            

            return View(userVM);
        }

        public async Task<IActionResult> DisplayImage()
        {
            int? userId = ViewData["UserId"] as int?;

         
            if (!userId.HasValue || userId.Value <= 0)
            {
                
                return NotFound(); 
            }
            var imageDetails = await _doctorlicenseRepository.GetLicenseByUserIdAsync(userId.Value);

            var imagePath = Path.Combine(imageDetails.externalPath, imageDetails.fileName);
            return PhysicalFile(imagePath, "image/png");
        }

        public async Task<IActionResult> ViewPending()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> pending = users.Where(u => u.role == 1);
            return View(pending);
        }

        public async Task<IActionResult> ViewApproved()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> pending = users.Where(u => u.role == 1);
            var approved = await DisplayApprovedHCProfessional();
            return View(pending);
            
        }

        public async Task<IActionResult> ViewDenied()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> declined = users.Where(u => u.role == 1);
            return View (declined);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            if (userdetails == null)
            {
                ErrorViewModel m = new ErrorViewModel();
                m.RequestId = Guid.NewGuid().ToString();
                return View("Error", m);
            }

            return View(userdetails);
        }

        [HttpPost, ActionName("Approve")]
        public async Task<IActionResult> ApproveDoctor(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            var doctorStatus = await _hcprofessionalRepository.GetByIdAsync(id);

            if (userdetails != null && doctorStatus != null)
            {
                // Update the user's status in the UserStatus table
                doctorStatus.verification = 1;
                 _hcprofessionalRepository.Update(doctorStatus);
            }
            else
            {
                return View("Error");
            }
            return RedirectToAction("ViewPending");
        }

        public async Task<IActionResult> DisplayApprovedHCProfessional()
        {
            // Query the HealthcareProfessional table for records with the specific verification value.
            var healthcareProfessionals = await _hcprofessionalRepository.GetHealthcareProfessionalsWithVerification();

            // Extract the User IDs from the matching healthcare professional records.
            var userIds = healthcareProfessionals.Select(h => h.userID).ToList();

            // Query the User table to get the users associated with the extracted User IDs.
            var usersWithVerification = await _userRepository.GetUsersWithIds(userIds);
            usersWithVerification.ToList();
            return View(usersWithVerification);
        }

        
    }
} 
