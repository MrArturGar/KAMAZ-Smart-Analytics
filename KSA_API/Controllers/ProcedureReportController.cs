using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProcedureReportController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<ProcedureReportController> _logger;

        public ProcedureReportController(ILogger<ProcedureReportController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostProcedureReport")]
        public void PostProcedureReport(ProcedureReport procedureReport)
        {
            if (procedureReport != null)
            {
                ProcedureReport tmp = GetProcedureReport(procedureReport.IdSession, procedureReport.DateStart, procedureReport.DateEnd, procedureReport.Type, procedureReport.Result); ;

                if (tmp == null)
                {
                    tmp = procedureReport;
                    Context.ProcedureReports.Add(tmp);
                    Context.SaveChanges();
                }
            }
        }

        [HttpGet("{idSession}, {dateEnd}, {type}", Name = "GetProcedureReport")]
        public ProcedureReport GetProcedureReport(int idSession,DateTime? dateStart, DateTime dateEnd, string type, bool? result)
        {
            return Context.ProcedureReports.Where(c => c.IdSession == idSession && c.DateStart == dateStart
            && c.DateEnd == dateEnd && c.Type == type && c.Result == result).SingleOrDefault();

        }

    }
}
