using Microsoft.AspNetCore.Mvc;
using Hart_Check_Official.DTO;
using System.Net.Http;
using WebApp_Doctor.Models;
using Newtonsoft.Json;
using WebApp_Doctor.Data;
using Hart_Check_Official.Models;
using Hart_Check_Official.Controllers;
using System.Text;

namespace WebApp_Doctor.Controllers
{
    public class CalendarSchedulingController : Controller
    {
        private readonly HttpClient _httpClient;

        public CalendarSchedulingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            try
            {
                var workOrders = await GetWorkOrders();

                if (workOrders == null)
                {
                    // Handle the case when workOrders is null
                    // You can return an error view or redirect to an error page
                    return View("Error");
                }

                var resources = await GetResources();

                var viewModel = new ScheduleViewModel
                {
                    WorkOrders = workOrders as List<WorkOrder>,
                    Resources = resources as List<Resource>
                };

                // Add logging to check the retrieved data
                /*  Console.WriteLine("Work Orders Count: " + viewModel.WorkOrders?.Count);
                  Console.WriteLine("Resources Count: " + viewModel.Resources?.Count);*/

                // Set the title of the view
                ViewData["Title"] = "Schedule";

                // Return the view with the viewModel as the model
                return View("Schedule", viewModel);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the action
                Console.WriteLine("Error in Schedule action: " + ex.Message);
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetResources()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7010/api/Resources");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Resource> resources = JsonConvert.DeserializeObject<List<Resource>>(responseBody);
                    return View(resources);
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

        [HttpGet]
        public async Task<IActionResult> GetFlatResources()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7010/api/Resources/Flat");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Resource> resources = JsonConvert.DeserializeObject<List<Resource>>(responseBody);
                    return View(resources);
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

        [HttpGet]
        public async Task<IActionResult> GetWorkOrders()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7010/api/WorkOrders");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<WorkOrder> workOrders = JsonConvert.DeserializeObject<List<WorkOrder>>(responseBody);
                    return View(workOrders);
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

        [HttpGet]
        public async Task<IActionResult> GetUnscheduledWorkOrders()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7010/api/WorkOrders/Unscheduled");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<WorkOrder> workOrders = JsonConvert.DeserializeObject<List<WorkOrder>>(responseBody);
                    return View(workOrders);
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

        [HttpGet]
        public async Task<IActionResult> GetWorkOrder(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7010/api/WorkOrders/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WorkOrder workOrder = JsonConvert.DeserializeObject<WorkOrder>(responseBody);
                    return View(workOrder);
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

        [HttpPut]
        public async Task<IActionResult> UpdateWorkOrder(int id, WorkOrderUpdateParams p)
        {
            try
            {
                string json = JsonConvert.SerializeObject(p);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7010/api/WorkOrders/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WorkOrder workOrder = JsonConvert.DeserializeObject<WorkOrder>(responseBody);
                    return View(workOrder);
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
        public async Task<IActionResult> CreateWorkOrder(PostWorkOrderParams p)
        {
            try
            {
                string json = JsonConvert.SerializeObject(p);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7010/api/WorkOrders", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WorkOrder workOrder = JsonConvert.DeserializeObject<WorkOrder>(responseBody);
                    return View(workOrder);
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
        public async Task<IActionResult> UnscheduleWorkOrder(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync($"https://localhost:7010/api/WorkOrders/{id}/Unschedule", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetWorkOrders");
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

        [HttpDelete]
        public async Task<IActionResult> DeleteWorkOrder(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7010/api/WorkOrders/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetWorkOrders");
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
