using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KAMAZ_Smart_Analytics.Controllers
{
    [Authorize]
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
            ProcedureReportListWeb list =await client.GetProcedureReportListWebAsync("Flash", null, null, null, null, weekOld, null, "asdads", 1000, 0);
            TempData["weekVehicle"] = list.Items.DistinctBy(c => c.Vin).Count();
            TempData["weekMaxVehicle"] = await client.GetVehicleCountAsync(weekOld, null);
            TempData["weekSuccessful"] = list.Items.Where(c => c.Result == true).Count();
            TempData["weekMaxSuccessful"] = list.Count;
            TempData["weekEcus"] = list.Items.DistinctBy(c => c.IdEcu).Count();
            TempData["weekMaxEcus"] = await client.GetEcuCountAsync();
            #endregion


            #region Chart2
            string[] fileArray = list.Items.DistinctBy(c => c.DataFiles).Where(c => !string.IsNullOrEmpty(c.DataFiles)).Select(c => c.DataFiles).ToArray();
            Dictionary<string, int> flashes = new Dictionary<string, int>();

            foreach(string fileName in fileArray)
            {
                flashes[fileName] = list.Items.Where(c=>c.DataFiles == fileName).Count();
            }

            flashes.Add("ads",12);//
            flashes.Add("aws", 14);
            flashes.Add("ass", 11);
            flashes.Add("acs", 13);
            flashes.Add("fdsdfv", 17);//
            flashes.Add("drfw", 12);
            flashes.Add("erfwe", 15);//
            flashes = flashes.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            int maxPart = 5;
            if (flashes.Count>= maxPart)
                flashes.Add("Other", 0);

            foreach (string key in flashes.Keys)
            {
                if (maxPart <= 1 && key != "Other")
                {
                    flashes["Other"] += flashes[key];
                    flashes.Remove(key);
                }
                maxPart--;
            }

            TempData["flashStrings"] = JsonConvert.SerializeObject(flashes.Keys);
            TempData["flashValues"] = JsonConvert.SerializeObject(flashes.Values);

            #endregion

            #region Table
            TempData["procedureReportList"] = list.Items.Take(10).ToArray();
            TempData["newSessions"] = (await client.GetSessionListWebAsync(null, null, null, null, null, null, null, null, null, null, null, null, null, "qwe", 6, 0)).Items.ToArray();
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