using Microsoft.AspNetCore.Mvc;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
