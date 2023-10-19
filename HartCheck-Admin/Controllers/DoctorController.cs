using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorrepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public DoctorController(IDoctorRepository doctorrepository, IHttpContextAccessor httpContextAccessor)
        {
            _doctorrepository = doctorrepository;
            _httpcontextAccessor = httpContextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Doctor> doctors = await _doctorrepository.GetAll();
            return View(doctors);
        }
    }
}
