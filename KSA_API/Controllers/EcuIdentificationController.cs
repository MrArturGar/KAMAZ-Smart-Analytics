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

        [HttpGet("{ecuId}, {identificationId}", Name = "GetEcuIdentification")]
        public EcuIdentification GetEcuIdentification(int ecuId, int identificationId)
        {
            return Context.EcuIdentifications.Where(c => c.IdEcu == ecuId && c.IdIdentifications == identificationId).SingleOrDefault();
        }


        [HttpPost(Name = "PostEcuIdentification")]
        public int PostEcuIdentification(EcuIdentification ecuIdentification)
        {
            EcuIdentification tmp = GetEcuIdentification(ecuIdentification.IdEcu, ecuIdentification.IdIdentifications);

            if (tmp == null)
            {
                tmp = ecuIdentification;
                Context.EcuIdentifications.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }
    }
}
