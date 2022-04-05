using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AoglonassReportController
    {
        KSA_DBContext Context;

        private readonly ILogger<AoglonassReportController> _logger;

        public AoglonassReportController(ILogger<AoglonassReportController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostAoglonassReport")]
        public int PostAoglonass(AoglonassReport aoglonassReport)
        {
            AoglonassReport tmp = GetAoglonassReport(aoglonassReport.IdSession, aoglonassReport.DateStart);

            if (tmp == null)
            {
                tmp = aoglonassReport;
                Context.AoglonassReports.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{idSession}, {dateStart}", Name = "GetAoglonassReport")]
        public AoglonassReport GetAoglonassReport(int idSession, DateTime dateStart)
        {
            return Context.AoglonassReports.Where(c => c.IdSession == idSession && c.DateStart == dateStart).SingleOrDefault(); ;
        }

        [HttpGet("{sessionId}", Name = "GetAoglonassReportBySessionId")]
        public AoglonassReport[] GetAoglonassReportBySessionId(int sessionId)
        {
            return Context.AoglonassReports.Where(c=>c.IdSession == sessionId).ToArray();
        }

    }
}
