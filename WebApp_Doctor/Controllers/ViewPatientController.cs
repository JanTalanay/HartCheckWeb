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
    public class ViewPatientController : Controller
    {
        private readonly HttpClient _httpClient;

        public ViewPatientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet] 
        public async Task<IActionResult> ViewPatientLists()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7010/api/ViewPatient");

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

        [HttpPost]
        public async Task<IActionResult> AdvancedSearch(string searchQuery)
        {
            try
            {
                // Make the HTTP request to the API endpoint with the search query
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7010/api/ViewPatient?searchQuery={searchQuery}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Users> allPatients = JsonConvert.DeserializeObject<List<Users>>(responseBody);

                    // Filter the patients based on the search query
                    List<Users> searchResults = allPatients.Where(patient =>
                        patient.firstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        patient.lastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        patient.email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) 
                    ).ToList();

                    return View("ViewPatientLists", searchResults);
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
