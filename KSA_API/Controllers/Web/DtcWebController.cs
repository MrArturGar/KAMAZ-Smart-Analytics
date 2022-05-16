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
    public class DtcWebController
    {
        KSA_DBContext Context;

        private readonly ILogger<DtcWebController> _logger;

        public DtcWebController(ILogger<DtcWebController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }
        [HttpGet("{sessionId:int}", Name = "GetDtcWeb")]
        public DtcWeb[] GetDtcWeb(int sessionId)
        {
            return Context.DtcWebs.Where(c => c.IdSession == sessionId).ToArray();
        }
    }
}
