using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Web;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class SessionController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;


        [Authorize]
        public async Task<IActionResult> List(string? vin, string? versionDb, int page = 1, SortSessionState sortOrder = SortSessionState.DateDesc)
        {
            ApiClientContext test = new ApiClientContext();



            int pageCount, entitesOnPage = 30;
            versionDb = versionDb=="Все"?null: versionDb;

            SessionList sessions = await client.GetSessionsWebAsync(vin, versionDb, sortOrder.ToString(), entitesOnPage, entitesOnPage * (page - 1));

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
    }
}
