using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Web;

namespace KAMAZ_Smart_Analytics.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;


        public async Task<IActionResult> List(string? vin, string? versionDb, int page = 1, SortSessionState sortOrder = SortSessionState.DateDesc)
        {
            int pageCount, entitesOnPage = 30;
            versionDb = versionDb=="Все"?null: versionDb;

            SessionListWeb sessions = await client.GetSessionListWebAsync(vin, versionDb, sortOrder.ToString(), entitesOnPage, entitesOnPage * (page - 1));

            pageCount = (int)Math.Ceiling((double)sessions.Count / (double)entitesOnPage);

            PageViewModel pageViewModel = new PageViewModel(sessions.Count, page, entitesOnPage);
            SessionListViewModel viewModel = new SessionListViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortSessionViewModel(sortOrder),
                FilterViewModel = new FilterSessionViewModel((await client.GetDbVersionsAsync()).ToList(), vin, versionDb),
                Objects = sessions.Items
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Index(int id)
        {
            Session session = await client.GetSessionByIdAsync(id);
            ViewBag.Session = session;
            ViewBag.ServiceCenter = await client.GetServiceCenterByIdAsync(session.IdServiceCenters);
            ViewBag.Vehicle = await client.GetVehicleByIdAsync(session.IdVehicle);
            Dictionary<string, List<IdentificationWeb>> identifications = new();
            foreach (IdentificationWeb ident in await client.GetIdentificationWebAsync(id))
            {
                if (identifications.ContainsKey(ident.Codifier))
                {
                    identifications[ident.Codifier].Add(ident);
                }
                else
                {
                    identifications.Add(ident.Codifier, new List<IdentificationWeb>());
                    identifications[ident.Codifier].Add(ident);
                }
            }
            string[] key = identifications.Keys.ToArray();
            ViewBag.Identifications = identifications;

            ViewBag.ProcedureReports = await client.GetProcedureReportWebAsync(id);
            ViewBag.AoglonassReports = await client.GetAoglonassReportBySessionIdAsync(id);

            return View();
        }
    }
}
