using HartCheck_Doctor_test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HartCheck_Doctor_test.FileUploadService;

namespace HartCheck_Doctor_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*private readonly IFileUploadService fileUploadService;
        public string filePath; */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                filePath = await fileUploadService.UploadFileAsync(file);
                Console.WriteLine(filePath);
                
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }*/
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}