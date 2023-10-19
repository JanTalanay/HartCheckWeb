using Hart_Check_Official.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebApp_Doctor.Models;
using System.Net.Http;
using Newtonsoft.Json;
using WebApp_Doctor.Data;
using Hart_Check_Official.Models;

namespace WebApp_Doctor.Controllers
{
    public class WebApp_ViewPatientController : Controller
    {
        private readonly HttpClient _httpClient;

        public WebApp_ViewPatientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet] // Add this attribute to explicitly specify the HTTP method
        public async Task<IActionResult> ViewPatientLists()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44319/api/ViewPatient");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Users> viewPatients = JsonConvert.DeserializeObject<List<Users>>(responseBody);
                    return View(viewPatients);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                return View("Error");
            }
        }
    }
}
/*private readonly HttpClient _httpClient;

public WebApp_ViewPatientController()
{
    _httpClient = new HttpClient();
    _httpClient.BaseAddress = new Uri("https://localhost:44319");
}

public async Task<IActionResult> ViewPatientLists()
{
    try
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/ViewPatient");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Users> viewPatients = JsonConvert.DeserializeObject<List<Users>>(responseBody);
            return View(viewPatients);
        }
        else
        {
            return View("Error");
        }
    }
    catch (Exception ex)
    {
        // Handle any exceptions that occur during the API request
        return View("Error");
    }
}
}
}
*/