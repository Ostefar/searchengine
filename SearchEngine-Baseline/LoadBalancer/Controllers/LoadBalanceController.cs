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
        public async Task<IActionResult> Get()
        {
            string nextService = loadBalancer.NextService();
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(nextService);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}