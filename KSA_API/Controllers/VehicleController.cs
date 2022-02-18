using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;


namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController
    {
        KSA_DBContext Context = new();
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetVehicle")]
        public Vehicle GetVehicle(string vin, string designNumber, string? iccid, string? iccidc, string? imei, string? type)
        {
            return Context.Vehicles.Where(c => c.Vin == vin && c.DesignNumber == designNumber && c.Iccid == iccid && c.Iccidc == iccidc && c.Imei == imei && c.Type == type).SingleOrDefault();
        }

        [HttpPost(Name = "PostVehicle")]
        public int PostVehicle(Vehicle vehicle)
        {
            Vehicle tmp = GetVehicle(vehicle.Vin, vehicle.DesignNumber, vehicle.Iccid, vehicle.Iccidc, vehicle.Imei, vehicle.Type);

            if (tmp == null)
            {
                tmp = vehicle;
                Context.Vehicles.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }
    }
}
