using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentificationController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<IdentificationController> _logger;

        public IdentificationController(ILogger<IdentificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostIdentification")]
        public int PostIdentification(Identification identification)
        {
            Context.Identifications.Add(identification);
            Context.SaveChanges();
            return identification.Id;
        }

        [HttpGet("{name}", Name = "GetIdentification")]
        public Identification GetIdentification(string name, string value)
        {
            return Context.Identifications.Where(c => c.Name == name && c.Value == value).SingleOrDefault();
        }
    }
}
