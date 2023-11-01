using Microsoft.AspNetCore.Mvc;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Google.Analytics.Data.V1Beta;
using Microsoft.AspNetCore.Authorization;
using HartCheck_Admin.Data;
using Microsoft.EntityFrameworkCore;
using HartCheck_Admin.Models;

namespace HartCheck_Admin.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly BetaAnalyticsDataClient _analyticsClient;
        private readonly ApplicationDbContext _context;

        /* public AnalyticsController(BetaAnalyticsDataClient analyticsClient)
        {
            _analyticsClient = analyticsClient;
        }*/
        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            int patientCount = await _context.Patients.Where(item => item.role == 0).CountAsync();
            ViewData["PatientCount"] = patientCount;

            int doctorcount = await _context.Patients.Where(item => item.role == 1).CountAsync();
            ViewData["DoctorCount"] = doctorcount;

            int bugreportcount = await _context.BugReports.CountAsync();
            ViewData["BugReportCount"] = bugreportcount;

            return View();
        }

        /*
                [HttpGet("report")]
                public async Task<IActionResult> GetAnalyticsReport()
                {
                    var request = new RunReportRequest
                    {
                        Property = "properties/" + PropertyId,
                        Dimensions = { new Dimension { Name = "date" }, },
                        Metrics = { new Metric { Name = "totalUsers" }, new Metric { Name = "newUsers" } },
                        DateRanges = { new DateRange { StartDate = "2021-04-01", EndDate = "today" }, },
                    };

                    var response = await _analyticsClient.RunReportAsync(request);

                    var result = response.Rows.Select(row =>
                        $"{row.DimensionValues[0].Value}, {row.MetricValues[0].Value}, {row.MetricValues[1].Value}");

                    return Ok(result);
                }*/


    }
}
