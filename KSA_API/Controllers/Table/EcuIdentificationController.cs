using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EcuIdentificationController
    {
        KSA_DBContext Context;

        private readonly ILogger<EcuIdentificationController> _logger;

        public EcuIdentificationController(ILogger<EcuIdentificationController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
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
