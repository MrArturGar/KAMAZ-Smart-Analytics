using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Table;
using Microsoft.AspNetCore.Authorization;
using KSA_API.Data;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CompositeController
    {
        KSA_DBContext Context;
        private readonly ILogger<CompositeController> _logger;

        public CompositeController(ILogger<CompositeController> logger, KSA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        [HttpPost(Name = "PostComposite")]
        public int PostComposite(Composite composite)
        {
            Composite tmp = GetComposite(composite.DesignNumber, composite.IdEcu);

            if (tmp == null)
            {
                tmp = composite;
                Context.Composites.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;
        }

        [HttpGet("{designNumber}, {idEcu}", Name = "GetComposite")]
        public Composite GetComposite(string designNumber, int idEcu)
        {
            return Context.Composites.Where(c => c.DesignNumber == designNumber && c.IdEcu == idEcu).SingleOrDefault();
        }
    }
}
