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
    public class ProcedureReportWebController
    {
        KSA_DBContext Context;

        private readonly ILogger<ProcedureReportWebController> _logger;

        public ProcedureReportWebController(ILogger<ProcedureReportWebController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpGet("{sessionId:int}", Name = "GetProcedureReportWeb")]
        public ProcedureReportWeb[] GetProcedureReportWeb(int sessionId)
        {
            return Context.ProcedureReportWebs.Where(c => c.IdSession == sessionId).ToArray();
        }

        //(type, vin, ecu, result, dateStart, dateEnd, sortOrder.ToString(), entitesOnPage, entitesOnPage* (page - 1))
        [HttpGet("{sortBy}, {take}, {skip}", Name = "GetProcedureReportListWeb")]
        public ProcedureReportListWeb GetProcedureReportListWeb(string? type, string? vin, string? ecu, bool? result, string? file, DateTime? dateStart, DateTime? dateEnd, string sortBy, int take, int skip)
        {
            IQueryable<ProcedureReportWeb> procedures = Context.ProcedureReportWebs;

            #region Filter
            if (!String.IsNullOrEmpty(type))
            {
                procedures = procedures.Where(p => p.Type == type);
            }
            if (!String.IsNullOrEmpty(vin))
            {
                procedures = procedures.Where(c => c.Vin == vin);
            }
            if (!String.IsNullOrEmpty(ecu))
            {
                procedures = procedures.Where(c => c.Codifier == ecu);
            }
            if (result != null)
            {
                procedures = procedures.Where(c => c.Result == result);
            }
            if (!String.IsNullOrEmpty(file))
            {
                procedures = procedures.Where(c => c.DataFiles == file);
            }
            if (dateStart != null)
            {
                procedures = procedures.Where(c => c.DateEnd >= dateStart);
            }
            if (dateEnd != null)
            {
                procedures = procedures.Where(c => c.DateEnd <= dateEnd);
            }
            #endregion


            #region Sort
            switch (sortBy)
            {
                case "DateAsc":
                    procedures = procedures.OrderBy(s => s.DateEnd);
                    break;
                case "TypeAsc":
                    procedures = procedures.OrderBy(s => s.Type);
                    break;
                case "TypeDesc":
                    procedures = procedures.OrderByDescending(s => s.Type);
                    break;
                case "VinAsc":
                    procedures = procedures.OrderBy(s => s.Vin);
                    break;
                case "VinDesc":
                    procedures = procedures.OrderByDescending(s => s.UsingVin);
                    break;
                case "EcuAsc":
                    procedures = procedures.OrderBy(s => s.Codifier);
                    break;
                case "EcuDesc":
                    procedures = procedures.OrderByDescending(s => s.Codifier);
                    break;
                case "ResultAsc":
                    procedures = procedures.OrderBy(s => s.Result);
                    break;
                case "ResultDesc":
                    procedures = procedures.OrderByDescending(s => s.Result);
                    break;
                case "FileAsc":
                    procedures = procedures.OrderBy(s => s.DataFiles);
                    break;
                case "FileDesc":
                    procedures = procedures.OrderByDescending(s => s.DataFiles);
                    break;
                default:
                    procedures = procedures.OrderByDescending(s => s.DateEnd);
                    break;
            }
            #endregion

            return new ProcedureReportListWeb
            {
                Count = procedures.Count(),
                Items = procedures.Skip(skip).Take(take).ToArray()
            };
        }
    }
}
