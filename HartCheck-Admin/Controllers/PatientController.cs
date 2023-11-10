using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Authorization;
using HartCheck_Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HartCheck_Admin.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHCProfessionalRepository _hcprofessionalRepository;
        private readonly IDoctorLicenseRepository _doctorlicenseRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly ApplicationDbContext _context;
        public PatientController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IHCProfessionalRepository hcprofessionalRepository, IDoctorLicenseRepository doctorlicenseRepository)
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
            IEnumerable<User> patients = users.Where(u => u.role == 0);
            return View(patients);
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
    }
}
