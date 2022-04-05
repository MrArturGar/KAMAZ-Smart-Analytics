using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionEcuIdentificationController
    {
        KSA_DBContext Context;

        private readonly ILogger<SessionEcuIdentificationController> _logger;

        public SessionEcuIdentificationController(ILogger<SessionEcuIdentificationController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostSessionEcuIdentification")]
        public int PostSessionEcuIdentification(SessionEcuidentification sessionEcuidentification)
        {
            SessionEcuidentification tmp = GetSessionEcuIdentification(sessionEcuidentification.IdSession, sessionEcuidentification.IdEcuidentifications);

            if (tmp == null)
            {
                tmp = sessionEcuidentification;
                Context.SessionEcuidentifications.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{idSession}, {idEcuidentifications}", Name = "GetSessionEcuIdentification")]
        public SessionEcuidentification GetSessionEcuIdentification(int idSession, int idEcuidentifications)
        {
            return Context.SessionEcuidentifications.Where(c => c.IdSession == idSession && c.IdEcuidentifications == idEcuidentifications).SingleOrDefault();
        }
    }
}
