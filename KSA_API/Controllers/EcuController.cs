﻿using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;

namespace KSA_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EcuController
    {
        KSA_DBContext Context;

        private readonly ILogger<EcuController> _logger;

        public EcuController(ILogger<EcuController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpGet("{codifier}", Name = "GetEcu")]
        public Ecu GetEcu(string codifier)
        {
            return Context.Ecus.Where(c => c.Codifier == codifier).SingleOrDefault();
        }

        [HttpPost(Name = "PostEcu")]
        public int PostEcu(Ecu ecu)
        {
            Ecu tmp = GetEcu(ecu.Codifier);

            if (tmp == null)
            {
                tmp = ecu;
                Context.Ecus.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }
    }
}
