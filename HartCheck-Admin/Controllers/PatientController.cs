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

        public async Task<IActionResult> Delete(int id)
        {
            var patientdetails = await _patientRepository.GetByIdAsync(id);
            if (patientdetails == null)
            {
                ErrorViewModel m = new ErrorViewModel();
                m.RequestId = Guid.NewGuid().ToString();
                return View("Error", m);
            }

            return View(patientdetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeletePatient(int id)
        {
            var patientdetails = await _patientRepository.GetByIdAsync(id);
            if (patientdetails == null)
            {
                return View("Error");
            }
            else
            {
                _patientRepository.Delete(patientdetails);
                return RedirectToAction("Index");
            }

        }
    }
}
