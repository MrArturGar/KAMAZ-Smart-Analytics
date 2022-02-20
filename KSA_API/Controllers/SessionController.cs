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

        [HttpGet(Name = "GetSessionsCount")]
        public int GetSessionsCount(string? vin)
        {
            if (vin == null)
                return Context.Sessions.Count();
            else
            {
                int[] vehicles = Context.Vehicles.Where(c => c.Vin == vin).Select(c => c.Id).ToArray();

                return Context.Sessions.Where(c => vehicles.Contains(c.IdVehicle)).Count();
            }
        }

        [HttpGet("{take}, {pick}",Name = "GetSessions")]
        public Session[] GetSessions(string? vin, int take, int pick)
        {
            if (vin == null)
                return Context.Sessions.Skip(pick).Take(take).ToArray();
            else
            {
                int[] vehicles = Context.Vehicles.Where(c => c.Vin == vin).Select(c => c.Id).ToArray();

                return Context.Sessions.Where(c => vehicles.Contains(c.IdVehicle)).Skip(pick).Take(take).ToArray();
            }
        }
    }

}
