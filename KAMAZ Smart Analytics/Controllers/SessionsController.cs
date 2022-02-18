using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class SessionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
