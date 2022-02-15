using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AoglonassReportController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<AoglonassReportController> _logger;

        public AoglonassReportController(ILogger<AoglonassReportController> logger)
        {
            _logger = logger;
        }

        [HttpPost( Name = "PostAoglonassReport")]
        public void PostAoglonass(AoglonassReport aoglonassReport)
        {
            if (aoglonassReport != null)
            {
                AoglonassReport tmp = GetAoglonassReport(aoglonassReport.IdSession, aoglonassReport.DateStart);

                if (tmp == null)
                {
                    tmp = aoglonassReport;
                    Context.AoglonassReports.Add(tmp);
                    Context.SaveChanges();
                }
            }
        }

        [HttpGet("{idSession}, {dateStart}", Name = "GetAoglonassReport")]
        public AoglonassReport GetAoglonassReport(int idSession, DateTime dateStart)
        {
            return Context.AoglonassReports.Where(c => c.IdSession == idSession && c.DateStart == dateStart).SingleOrDefault(); ;
        }
    }
}
