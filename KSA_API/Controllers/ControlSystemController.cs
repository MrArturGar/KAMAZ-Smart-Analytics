using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
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

        [HttpPost(Name = "PostControlSystem")]
        public int PostControlSystem(ControlSystem controlSystem)
        {
            ControlSystem tmp = GetControlSystem(controlSystem.Name, controlSystem.Domain);

            if (tmp == null)
            {
                tmp = controlSystem;
                Context.ControlSystems.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }
    }
}
