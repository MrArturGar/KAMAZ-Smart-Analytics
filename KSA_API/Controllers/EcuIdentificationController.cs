using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EcuIdentificationController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<EcuIdentificationController> _logger;

        public EcuIdentificationController(ILogger<EcuIdentificationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{ecu}, {identification}", Name = "GetEcuIdentificationController")]
        public EcuIdentification GetEcuIdentificationController(int ecuId, int identificationId)
        {
            return Context.EcuIdentifications.Where(c => c.IdEcu == ecuId && c.IdIdentifications == identificationId).SingleOrDefault();
        }

    }
}
