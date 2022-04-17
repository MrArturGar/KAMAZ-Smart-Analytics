using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Table;
using TableModelLibrary.Web;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SessionController
    {
        KSA_DBContext Context;

        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostSession")]
        public int PostSession(Session session)
        {
            Session tmp = GetSessionByName(session.SessionsName);

            if (tmp == null)
            {
                tmp = session;
                Context.Sessions.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;

        }

        [HttpGet("{sessionName}", Name = "GetSessionByName")]
        public Session GetSessionByName(string sessionName)
        {
            return Context.Sessions.Where(c => c.SessionsName == sessionName).SingleOrDefault();
        }

        [HttpGet("{id:int}", Name = "GetSessionById")]
        public Session GetSessionById(int id)
        {
            return Context.Sessions.Where(c => c.Id == id).Single();
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

        [HttpGet("{sortBy}, {take}, {skip}", Name = "GetSessionListWeb")]
        public SessionListWeb GetSessionListWeb(string? vin, string? versionDb, string sortBy, int take, int skip)
        {
            IQueryable<Session> sessions = Context.Sessions;

            #region Filter
            if (!String.IsNullOrEmpty(versionDb))
            {
                sessions = sessions.Where(p => p.VersionDb == versionDb);
            }
            if (!String.IsNullOrEmpty(vin))
            {
                int[] vehicles = Context.Vehicles.Where(c => c.Vin == vin).Select(c => c.Id).ToArray();
                sessions = sessions.Where(c => vehicles.Contains(c.IdVehicle));
            }
            #endregion

            switch (sortBy)
            {
                case "TypeAsc":
                    sessions = sessions.OrderBy(s => s.SessionsName);
                    break;
                case "TypeDesc":
                    sessions = sessions.OrderByDescending(s => s.SessionsName);
                    break;
                case "VinAsc":
                    sessions = sessions.OrderBy(s => s.SessionsName);
                    break;
                case "VinDesc":
                    sessions = sessions.OrderByDescending(s => s.SessionsName);
                    break;
                case "DateAsc":
                    sessions = sessions.OrderBy(s => s.Date);
                    break;
                case "DateDesc":
                    sessions = sessions.OrderByDescending(s => s.Date);
                    break;
                case "VersionDbDesc":
                    sessions = sessions.OrderByDescending(s => s.VersionDb);
                    break;
                case "VersionDbAsc":
                    sessions = sessions.OrderBy(s => s.VersionDb);
                    break;
                default:
                    sessions = sessions.OrderByDescending(s => s.Date);
                    break;
            }


            return new SessionListWeb
            {
                Count = sessions.Count(),
                Items = sessions.Skip(skip).Take(take).ToArray()
            };
        }

        [HttpGet("{idVehicle:int}/GetSessionsByVehicleId")]
        public Session[] GetSessionsByVehicleId(int idVehicle)
        {
            return Context.Sessions.Where(c => c.IdVehicle == idVehicle).OrderByDescending(c => c.Date).ToArray();
        }

        [HttpGet(nameof(GetDbVersions))]
        public string?[] GetDbVersions()
        {
            return Context.Sessions.Select(c => c.VersionDb).Distinct().OrderBy(c => c).ToArray();
        }
    }

}
