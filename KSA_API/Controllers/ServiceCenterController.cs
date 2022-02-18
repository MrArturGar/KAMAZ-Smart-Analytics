using Microsoft.AspNetCore.Mvc;
using TableModelLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KSA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCenterController
    {
        KSA_DBContext Context = new();

        private readonly ILogger<ServiceCenterController> _logger;

        public ServiceCenterController(ILogger<ServiceCenterController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostServiceCenter")]
        public int PostServiceCenter(ServiceCenter serviceCenter)
        {
            ServiceCenter tmp = Context.ServiceCenters.Where(c => c.Name == serviceCenter.Name && c.Address == serviceCenter.Address
            && c.City == serviceCenter.City && c.Country == serviceCenter.Country && c.Postcode == serviceCenter.Postcode
            && c.Region == serviceCenter.Region && c.Username == serviceCenter.Username && c.Status == serviceCenter.Status
            && c.DilerTr == serviceCenter.DilerTr).SingleOrDefault();

            if (tmp == null)
            {
                tmp = serviceCenter;
                Context.ServiceCenters.Add(tmp);
                Context.SaveChanges();
            }
            return tmp.Id;

        }

        [HttpGet("{username}", Name = "GetServiceCenter")]
        public ServiceCenter GetServiceCenter(string username)
        {
            var users = Context.ServiceCenters.Where(c => c.Username == username).ToList();


            if (users.Count == 1)
                return users[0];
            else if (users.Count > 1)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Status == "")
                        return users[i];

                    if (i + 1 == users.Count)
                        return users[i];
                }
            }

            return null;
        }
    }
}
