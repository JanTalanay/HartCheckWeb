using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public PatientController(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor)
        {
            _patientRepository = patientRepository;
            _httpcontextAccessor = httpContextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Patient> patients = await _patientRepository.GetAll();
            return View(patients);
        }
    }
}
