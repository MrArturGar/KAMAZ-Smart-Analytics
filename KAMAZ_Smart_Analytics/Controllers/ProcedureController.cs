using KAMAZ_Smart_Analytics.Data;
using Microsoft.AspNetCore.Mvc;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class ProcedureController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;

        //public async Task<IActionResult> List(string? type, bool? result, DateTime? dateStart, DateTime? dateEnd, int page = 1, SortProcedureState sortOrder = SortProcedureState.DateDesc)
        //{
        //    return View();
        //}
    }
}
