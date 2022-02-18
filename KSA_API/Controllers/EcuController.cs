using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EcuController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<EcuController> _logger;

        public EcuController(ILogger<EcuController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{codifier}", Name = "GetEcu")]
        public Ecu GetEcu(string codifier)
        {
            return Context.Ecus.Where(c => c.Codifier == codifier).SingleOrDefault();
        }

        [HttpPost(Name = "PostEcu")]
        public int PostEcu(Ecu ecu)
        {
            Ecu tmp = GetEcu(ecu.Codifier);

            if (tmp == null)
            {
                tmp = ecu;
                Context.Ecus.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }
    }
}
