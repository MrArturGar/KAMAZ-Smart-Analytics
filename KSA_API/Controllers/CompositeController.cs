using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

namespace KSA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompositeController
    {
        KSA_DBContext Context = new();
        private readonly ILogger<CompositeController> _logger;

        public CompositeController(ILogger<CompositeController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostComposite")]
        public void PostComposite(Composite composite)
        {
            if (composite != null)
            {
                Composite tmp = GetComposite(composite.DesignNumber, composite.IdEcu);

                if (tmp == null)
                {
                    tmp = composite;
                    Context.Composites.Add(tmp);
                    Context.SaveChanges();
                }
            }
        }

        [HttpGet("{designNumber}, {idEcu}", Name = "GetComposite")]
        public Composite GetComposite(string designNumber, int idEcu)
        {
            return Context.Composites.Where(c => c.DesignNumber == designNumber && c.IdEcu == idEcu).SingleOrDefault();
        }
    }
}
