using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Table;
using TableModelLibrary.Web;
using KSA_API.Data;
using Microsoft.EntityFrameworkCore;

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
            //Session tmp = GetSessionByName(session.SessionsName);
            Session tmp = Context.Sessions.Where(c => c.Id == session.Id).SingleOrDefault();

            if (tmp == null)
            {
                Context.Sessions.Add(session);
            }
            else
            {
                Context.Entry(tmp).CurrentValues.SetValues(session);
            }
            Context.SaveChanges();
            return session.Id;///////////////////////

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
