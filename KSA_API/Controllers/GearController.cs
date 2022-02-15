using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;
namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GearController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<GearController> _logger;

        public GearController(ILogger<GearController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}, {domain}", Name = "GetGear")]
        public Gear GetGear(string name, string domain)
        {
            return Context.Systems.Where(c => c.Name == name && c.Domain == domain).SingleOrDefault();
        }
    }
}
