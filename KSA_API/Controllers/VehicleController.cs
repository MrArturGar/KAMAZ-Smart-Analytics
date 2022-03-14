using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using TableModelLibrary.Web;
using Microsoft.AspNetCore.Authorization;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class VehicleController
    {
        KSA_DBContext Context = new();
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
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

        [HttpGet(Name = "GetVehicle")]
        public Vehicle GetVehicle(string vin, string designNumber, string? iccid, string? iccidc, string? imei, string? type)
        {
            return Context.Vehicles.Where(c => c.Vin == vin && c.DesignNumber == designNumber && c.Iccid == iccid && c.Iccidc == iccidc && c.Imei == imei && c.Type == type).SingleOrDefault();
        }

        [HttpGet("{id}", Name = "GetVehicleById")]
        public Vehicle GetVehicleById(int id)
        {
            return Context.Vehicles.Where(c => c.Id == id).Single();
        }

        [HttpGet("{sortBy}, {take}, {skip}", Name = "GetVehicleListWeb")]
        public VehicleListWeb GetVehicleListWeb(string? vin, string? design_number,string? iccid, string? iccidc, string? imei, string? type, string sortBy, int take, int skip)
        {
            IQueryable<Vehicle> vehicles = Context.Vehicles;
            if (!String.IsNullOrEmpty(vin))
            {
                vehicles = vehicles.Where(p => p.Vin == vin);
            }
            if (!String.IsNullOrEmpty(design_number))
            {
                vehicles = vehicles.Where(p => p.DesignNumber == design_number);
            }
            if (!String.IsNullOrEmpty(iccid))
            {
                vehicles = vehicles.Where(p => p.Iccid == iccid);
            }
            if (!String.IsNullOrEmpty(iccidc))
            {
                vehicles = vehicles.Where(p => p.Iccidc == iccidc);
            }
            if (!String.IsNullOrEmpty(imei))
            {
                vehicles = vehicles.Where(p => p.Imei == imei);
            }
            if (!String.IsNullOrEmpty(type))
            {
                vehicles = vehicles.Where(p => p.Type == type);
            }


            switch (sortBy)
            {
                case "VinAsc":
                    vehicles = vehicles.OrderBy(s => s.Vin);
                    break;
                case "VinDesc":
                    vehicles = vehicles.OrderByDescending(s => s.Vin);
                    break;
                case "Design_numberAsc":
                    vehicles = vehicles.OrderBy(s => s.DesignNumber);
                    break;
                case "Design_numberDesc":
                    vehicles = vehicles.OrderByDescending(s => s.DesignNumber);
                    break;
                case "IccidAsc":
                    vehicles = vehicles.OrderBy(s => s.Iccid);
                    break;
                case "IccidDesc":
                    vehicles = vehicles.OrderByDescending(s => s.Iccid);
                    break;
                case "IccidcAsc":
                    vehicles = vehicles.OrderBy(s => s.Iccidc);
                    break;
                case "IccidcDesc":
                    vehicles = vehicles.OrderByDescending(s => s.Iccidc);
                    break;
                case "ImeiAsc":
                    vehicles = vehicles.OrderBy(s => s.Imei);
                    break;
                case "ImeiDesc":
                    vehicles = vehicles.OrderByDescending(s => s.Imei);
                    break;
                case "TypeAsc":
                    vehicles = vehicles.OrderBy(s => s.Type);
                    break;
                case "TypeDesc":
                    vehicles = vehicles.OrderByDescending(s => s.Type);
                    break;
                default:
                    vehicles = vehicles.OrderByDescending(s => s.Vin);
                    break;
            }


            return new VehicleListWeb
            {
                Count = vehicles.Count(),
                Items = vehicles.Skip(skip).Take(take).ToArray()
            };
        }
    }
}
