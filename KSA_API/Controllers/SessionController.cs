using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostSession")]
        public int PostSession(Session session)
        {
            Session tmp = GetSession(session.SessionsName);

            if (tmp == null)
            {
                tmp = session;
                Context.Sessions.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;

        }

        [HttpGet("{sessionName}", Name = "GetSession")]
        public Session GetSession(string sessionName)
        {
            return Context.Sessions.Where(c => c.SessionsName == sessionName).SingleOrDefault();
        }
    }

}
