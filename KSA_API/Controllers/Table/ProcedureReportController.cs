using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using TableModelLibrary.Web;
using Microsoft.AspNetCore.Authorization;
using KSA_API.Data;

namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProcedureReportController
    {
        KSA_DBContext Context;

        private readonly ILogger<ProcedureReportController> _logger;

        public ProcedureReportController(ILogger<ProcedureReportController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostProcedureReport")]
        public int PostProcedureReport(ProcedureReport procedureReport)
        {
            ProcedureReport tmp = GetProcedureReport(procedureReport.IdSession, procedureReport.DateStart, procedureReport.DateEnd, procedureReport.Type, procedureReport.Result); ;

            if (tmp == null)
            {
                tmp = procedureReport;
                Context.ProcedureReports.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{idSession:int}, {dateEnd}, {type}", Name = "GetProcedureReport")]
        public ProcedureReport GetProcedureReport(int idSession, DateTime? dateStart, DateTime dateEnd, string type, bool? result)
        {
            return Context.ProcedureReports.Where(c => c.IdSession == idSession && c.DateStart == dateStart
            && c.DateEnd == dateEnd && c.Type == type && c.Result == result).SingleOrDefault();

        }

        [HttpGet(nameof(GetProcedureReportCount))]
        public int GetProcedureReportCount(DateTime? dateStart, DateTime? dateEnd, bool? flash)
        {
            IQueryable<ProcedureReport> procedureReports = Context.ProcedureReports;


            if (dateStart != null)
            {
                procedureReports = procedureReports.Where(c => c.DateEnd >= dateStart);
            }
            if (dateEnd != null)
            {
                procedureReports = procedureReports.Where(c => c.DateEnd <= dateEnd);
            }
            if (flash != null)
            {
                procedureReports = flash==true ? procedureReports.Where(c => c.Type == "Flash") : procedureReports.Where(c => c.Type == "Test");
            }

            return procedureReports.Count();

        }
    }
}
