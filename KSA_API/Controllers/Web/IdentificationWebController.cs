﻿using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using TableModelLibrary.Web;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IdentificationWebController
    {
        KSA_DBContext Context;

        private readonly ILogger<IdentificationWebController> _logger;

        public IdentificationWebController(ILogger<IdentificationWebController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }
        [HttpGet("{sessionId:int}", Name = "GetIdentificationWeb")]
        public IdentificationWeb[] GetIdentificationWeb(int sessionId)
        {
            return Context.IdentificationWebs.Where(c => c.IdSession == sessionId).ToArray();
        }
    }
}
