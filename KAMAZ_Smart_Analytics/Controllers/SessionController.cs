using KAMAZ_Smart_Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class SessionController : Controller
    {
        swaggerClient client = new swaggerClient("https://localhost:7234/", new HttpClient());

        [Authorize]
        public IActionResult List(int? page)
        {
            int pageNum = page ?? 1;

            int pageCount, entitesOnPage = 30;

            Task<int> sessionsCount = client.GetSessionsCountAsync(null);
            sessionsCount.Wait();

            int entiteCount = sessionsCount.Result;
            pageCount = (int)Math.Ceiling((double)entiteCount / (double)entitesOnPage);


            Task<ICollection<Session>> sessions = client.GetSessionsAsync(null, entiteCount, 0);
            sessions.Wait();

            ViewBag.PageCount = pageCount;
            ViewBag.PageNumber = pageNum;
            //ViewBag.Sessions = sessions.Result;

            return View(sessions.Result.ToPagedList(pageNum, 30));
            //return View(students.ToPagedList(pageNumber, pageSize));
        }
    }
}
