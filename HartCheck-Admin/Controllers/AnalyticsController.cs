using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
