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
    public class SessionDtcsController
    {
        KSA_DBContext Context;

        private readonly ILogger<SessionDtcsController> _logger;

        public SessionDtcsController(ILogger<SessionDtcsController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostSessionDtc")]
        public int PostSessionDtc(SessionDtc sessionDtc)
        {
            SessionDtc tmp = GetSessionDtc(sessionDtc.IdDtc, sessionDtc.IdSession, sessionDtc.Status);
            if (tmp == null)
            {
                tmp = sessionDtc;
                Context.SessionDtcs.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{name}", Name = "GetSessionDtc")]
        public SessionDtc GetSessionDtc(int idDtc, int idSession, bool? status)
        {
            return Context.SessionDtcs.Where(c => c.IdDtc == idDtc && c.IdSession == idSession && c.Status == status).SingleOrDefault();
        }
    }
}
