using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Table;
using TableModelLibrary.Web;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SessionListWebController
    {
        KSA_DBContext Context;

        private readonly ILogger<SessionListWebController> _logger;

        public SessionListWebController(ILogger<SessionListWebController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }
        [HttpGet("{sortBy}, {take}, {skip}", Name = "GetSessionListWeb")]
        public SessionListWeb GetSessionListWeb(string? vin, string? versionDb, string? type, string? username, string? vcisn,
            double? mileageStart, double? mileageEnd, bool? hasIdentification, bool? hasDtc, bool? hasTests, bool? hasFlash, DateTime? dateStart, DateTime? dateEnd, string sortBy, int take, int skip)
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
            if(type != null)
            {
                int[] vehicles = Context.Vehicles.Where(c => c.Type == type).Select(c => c.Id).ToArray();
                sessions = sessions.Where(c => vehicles.Contains(c.IdVehicle));
            }
            if (username != null)
            {
                int[] users = Context.ServiceCenters.Where(c => c.Username == username).Select(c => c.Id).ToArray();
                sessions = sessions.Where(c => users.Contains(c.IdServiceCenters));
            }
            if (vcisn != null)
            {
                sessions = sessions.Where(c=>c.Vcisn == vcisn);
            }
            if (mileageStart != null)
            {
                sessions = sessions.Where(c => c.Mileage >= mileageStart);
            }
            if (mileageEnd != null)
            {
                sessions = sessions.Where(c => c.Mileage <= mileageEnd);
            }
            if (hasIdentification != null)
            {
                sessions = sessions.Where(c => c.HasIdentifications == hasIdentification);
            }
            if (hasDtc != null)
            {
                sessions = sessions.Where(c => c.HasDtc == hasDtc);
            }
            if (hasTests != null)
            {
                sessions = sessions.Where(c => c.HasTests == hasTests);
            }
            if (hasFlash != null)
            {
                sessions = sessions.Where(c => c.HasFlash == hasFlash);
            }
            if (dateStart != null)
            {
                sessions = sessions.Where(c=>c.Date >= dateStart);
            }
            if (dateEnd != null)
            {
                sessions = sessions.Where(c => c.Date <= dateEnd);
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

    }

}
