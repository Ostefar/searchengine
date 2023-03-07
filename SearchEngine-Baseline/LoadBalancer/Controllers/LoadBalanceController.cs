using Common;
using LoadBalancer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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
        public async Task<SearchResult> Search(string terms, int numberOfResults)
        {
            var serviceUrl = loadBalancer.NextService();
            var requestUrl = $"{serviceUrl}/Search?terms={terms}&numberOfResults={numberOfResults}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Deserialize API response
            var apiResponse = JsonConvert.DeserializeObject<SearchResult>(content);

            // Map API response to domain model
            var documents = apiResponse.Documents.Select((d, i) => new Document
            {
                Id = i + 1,
                Path = d.Path,
                NumberOfOccurences = d.NumberOfOccurences
            }).ToList();

            // Create new instance of SearchResult
            var result = new SearchResult
            {
                ElapsedMilliseconds = apiResponse.ElapsedMilliseconds,
                IgnoredTerms = apiResponse.IgnoredTerms,
                Documents = documents
            };

            Console.Write(result.ToString() + "SeekOrigin meee");

            return apiResponse;
        }
    }
    
}