using KAMAZ_Smart_Analytics.Data;
using KAMAZ_Smart_Analytics.Models.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Web;

namespace KAMAZ_Smart_Analytics.Controllers
{
    public class VehicleController : Controller
    {
        swaggerClient client = new ApiClientContext().GetClient;


        [Authorize]
        public async Task<IActionResult> List(string? vin, string? design_number, string? iccid, string? iccidc, string? imei, string? type, int page = 1, SortVehicleState sortOrder = SortVehicleState.TypeDesc)
        {
            int pageCount, entitesOnPage = 30;

            VehicleList vehicles = await client.GetVehiclesWebAsync(vin, design_number, iccid, iccidc, imei, type, sortOrder.ToString(), entitesOnPage, entitesOnPage * (page - 1));

            pageCount = (int)Math.Ceiling((double)vehicles.Count / (double)entitesOnPage);

            PageViewModel pageViewModel = new PageViewModel(vehicles.Count, page, entitesOnPage);
            VehicleListViewModel viewModel = new VehicleListViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortVehicleViewModel(sortOrder),
                FilterViewModel = new FilterVehicleViewModel(vin, design_number, iccid, iccidc, imei, type),
                Objects = vehicles.Items
            };

            return View(viewModel);
        }
    }
}
