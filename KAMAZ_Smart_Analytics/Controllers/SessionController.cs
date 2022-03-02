using KAMAZ_Smart_Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Web;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class SessionController : Controller
    {
        swaggerClient client = new swaggerClient("https://localhost:7234/", new HttpClient()
        {
            
        });

        [Authorize]
        public async Task<IActionResult> List(int page = 1)
        {
            int pageCount, entitesOnPage = 30;

            int sessionsCount = await client.GetSessionsCountAsync(null);

            int entiteCount = sessionsCount;
            pageCount = (int)Math.Ceiling((double)entiteCount / (double)entitesOnPage);


            ICollection<Session> sessions = await client.GetSessionsAsync(null, entitesOnPage, (page - 1) * entitesOnPage);

            PageViewModel pageViewModel = new PageViewModel(sessionsCount, page, entitesOnPage);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Objects = sessions
            };

            return View(viewModel);
            //return View(students.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public async Task<IActionResult> List(string? vin, string? versionDb, int page = 1, SortSessionState sortOrder = SortSessionState.DateDesc)
        {
            int pageCount, entitesOnPage = 30;
            versionDb = versionDb=="Все"?null: versionDb;

            SessionList sessions = await client.GetSessionWebAsync(vin, versionDb, sortOrder.ToString(), entitesOnPage, entitesOnPage * (page - 1));

            pageCount = (int)Math.Ceiling((double)sessions.Count / (double)entitesOnPage);

            PageViewModel pageViewModel = new PageViewModel(sessions.Count, page, entitesOnPage);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortSessionViewModel(sortOrder),
                FilterViewModel = new FilterSessionViewModel((await client.GetDbVersionsAsync()).ToList(), vin, versionDb),
                Objects = sessions.Items
            };

            return View(viewModel);
            //return View(students.ToPagedList(pageNumber, pageSize));
        }
    }
}
