using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class BugReportController : Controller
    {
        private readonly IBugReportRepository _bugreportRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public BugReportController(IBugReportRepository bugreportRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bugreportRepository = bugreportRepository;
            _httpcontextAccessor = httpContextAccessor;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BugReport> resources = await _bugreportRepository.GetAll();
            return View(resources);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var bugreportdetails = await _bugreportRepository.GetByIdAsync(id);
            if (bugreportdetails == null)
            {
                return View("Error");
            }
            else
            {
                _bugreportRepository.Delete(bugreportdetails);
                return RedirectToAction("Index");
            }

        }


    }
}
