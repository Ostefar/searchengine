using LoadBalancer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadBalancerController : ControllerBase
    {
        private readonly ILoadBalancer loadBalancer;

        public LoadBalancerController(ILoadBalancer loadBalancer)
        {
            this.loadBalancer = loadBalancer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string nextService = loadBalancer.NextService();
            // Use nextService to make a request to the API instance
            return Ok(nextService);
        }
    }
}