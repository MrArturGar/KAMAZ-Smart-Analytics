using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class HomeController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now.Date;
            DateTime yest = now.AddDays(-1);
            TempData["todaySession"] = await client.GetSessionsCountAsync(null, yest, now);
            TempData["yestSession"] = await client.GetSessionsCountAsync(null, now.AddDays(-2), yest);
            TempData["todayVehicle"] = await client.GetVehicleCountAsync(yest, now);
            TempData["yestVehicle"] = await client.GetVehicleCountAsync(now.AddDays(-2), yest);
            TempData["todayFlash"] = await client.GetProcedureReportCountAsync(yest, now, true);
            TempData["yestFlash"] = await client.GetProcedureReportCountAsync(now.AddDays(-2), yest, true);

            #region Chart1
            int mounthCount = 10;
            int[] mounthValue = new int[mounthCount];
            string[] mounthString = new string[mounthCount];
            for (int i = 0; i < 10; i++)
            {
                mounthString[i] = now.AddMonths(-i).ToString("MMM");
                if (i == 0)
                {
                    mounthValue[i] = await client.GetProcedureReportCountAsync(now, null, true);
                }
                else
                {
                    mounthValue[i] = await client.GetProcedureReportCountAsync(now.AddMonths(-i), now.AddMonths(-(i-1)), true);
                }
            }

            TempData["mounthStrings"] = JsonConvert.SerializeObject(mounthString.Reverse().ToArray());
            TempData["mounthValue"] = JsonConvert.SerializeObject(mounthValue.Reverse().ToArray());


            DateTime weekOld = now.AddDays(-7);
            ProcedureReportListWeb list =await client.GetProcedureReportListWebAsync("Flash", null, null, null, null, weekOld, null, "asdads", 100, 0);
            TempData["weekVehicle"] = list.Items.DistinctBy(c => c.Vin).Count();
            TempData["weekMaxVehicle"] = await client.GetVehicleCountAsync(weekOld, null);
            TempData["weekSuccessful"] = list.Items.Where(c => c.Result == true).Count();
            TempData["weekMaxSuccessful"] = list.Count;
            TempData["weekEcus"] = list.Items.DistinctBy(c => c.IdEcu).Count();
            TempData["weekMaxEcus"] = await client.GetEcuCountAsync();
            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}