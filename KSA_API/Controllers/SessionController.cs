using Microsoft.AspNetCore.Mvc;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController
    {
        Views.KSA_DBContext db = new();

        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{vin}")]
        public IEnumerable<Views.Session> GetByVin(string vin)
        {
            return db.Sessions.Where(s => s.IdVehicleNavigation.Vin == vin).ToArray();
        }

        [HttpGet("{fromDate}, {untilDate}")]
        public IEnumerable<Views.Session> GetByDate(DateTime fromDate, DateTime untilDate)
        {
            return db.Sessions.Where(s => s.Date >= fromDate && s.Date <= untilDate).ToArray();
        }
    }

}
