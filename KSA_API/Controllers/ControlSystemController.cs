using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;
namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ControlSystemController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<ControlSystemController> _logger;

        public ControlSystemController(ILogger<ControlSystemController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}, {domain}", Name = "GetControlSystem")]
        public ControlSystem GetControlSystem(string name, string domain)
        {
            return Context.ControlSystems.Where(c => c.Name == name && c.Domain == domain).SingleOrDefault();
        }
    }
}
