using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models.List;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TableModelLibrary.Web;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class ProcedureReportController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public ProcedureReportController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> List(string? type, string? vin, string? ecu, string? result, string? file,
            DateTime? dateStart,  DateTime? dateEnd, int page = 1, SortProcedureReportState sortOrder = SortProcedureReportState.DateDesc)
        {
            int pageCount, entitesOnPage = 30;
            type = type == _sharedLocalizer["All"] ? null : type;
            result = result == _sharedLocalizer["All"] ? null : result;


            bool? resultBool = null;
            if (result == "True")
                resultBool = true;
            else if (result == "False")
                resultBool = false;


            ProcedureReportListWeb procedures = await client.GetProcedureReportListWebAsync(type, vin, ecu, resultBool, file, dateStart, dateEnd, sortOrder.ToString(), entitesOnPage, entitesOnPage * (page - 1));

            pageCount = (int)Math.Ceiling((double)procedures.Count / (double)entitesOnPage);

            PageViewModel pageViewModel = new PageViewModel(procedures.Count, page, entitesOnPage);
            ProcedureReportListViewModel viewModel = new ProcedureReportListViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortProcedureReportViewModel(sortOrder),
                FilterViewModel = new FilterProcedureReportViewModel(new List<string> { _sharedLocalizer["All"], _sharedLocalizer["Flash"], _sharedLocalizer["Test"]},
                                                                     new List<string> { _sharedLocalizer["All"], _sharedLocalizer["True"], _sharedLocalizer["False"]},
                                                                     type, vin, ecu, result, file, dateStart, dateEnd),
                Objects = procedures.Items
            };

            return View(viewModel);
        }
    }
}
