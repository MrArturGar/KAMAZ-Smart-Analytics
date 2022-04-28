using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using KSA_API.Data;

namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ControlSystemController
    {
        KSA_DBContext Context;

        private readonly ILogger<ControlSystemController> _logger;

        public ControlSystemController(ILogger<ControlSystemController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
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
