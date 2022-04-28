using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Web;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IdentificationController
    {
        KSA_DBContext Context;

        private readonly ILogger<IdentificationController> _logger;

        public IdentificationController(ILogger<IdentificationController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostIdentification")]
        public int PostIdentification(Identification identification)
        {
            Identification tmp = GetIdentification(identification.Name, identification.Value);
            if (tmp == null)
            {
                tmp = identification;
                Context.Identifications.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{name}", Name = "GetIdentification")]
        public Identification GetIdentification(string name, string? value)
        {
            return Context.Identifications.Where(c => c.Name == name && c.Value == value).SingleOrDefault();
        }
    }
}
