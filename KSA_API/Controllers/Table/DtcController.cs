using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Web;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DtcController
    {
        KSA_DBContext Context;

        private readonly ILogger<DtcController> _logger;

        public DtcController(ILogger<DtcController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostDtc")]
        public int PostDtc(Dtc dtc)
        {
            Dtc tmp = GetDtc(dtc.Code, dtc.TroubleCode, dtc.VehicleType);
            if (tmp == null)
            {
                tmp = dtc;
                Context.Dtcs.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet(Name = "GetDtc")]
        public Dtc GetDtc(int code, string troubleCode, string vehicleType)
        {
            return Context.Dtcs.Where(c => c.Code == code && c.TroubleCode == troubleCode && c.VehicleType == vehicleType).SingleOrDefault();
        }
    }
}
