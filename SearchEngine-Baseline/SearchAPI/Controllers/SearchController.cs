using Common;
using ConsoleSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<SearchResult> Search(string terms, int numberOfResults) 
        {
            var mSearchLogic = new SearchLogic(new Database());
            var result = new SearchResult();

            var wordIds = new List<int>();
            var searchTerms = terms.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in searchTerms)
            {
                int id = mSearchLogic.GetIdOf(t);
                if (id != -1)
                {
                    wordIds.Add(id);
                }
                else
                {
                    result.IgnoredTerms.Add(t);
                }
            }

            DateTime start = DateTime.Now;

            var docIds = await mSearchLogic.GetDocuments(wordIds);

            // get details amount of number of result             
            var top = new List<int>();
            foreach (var p in docIds.GetRange(0, Math.Min(numberOfResults, docIds.Count)))
            {
                top.Add(p.Key);
            }

            result.ElapsepMilliseconds = (DateTime.Now - start).TotalMilliseconds;

            int idx = 0;
            foreach (var doc in await mSearchLogic.GetDocumentDetails(top))
            {
                result.Documents.Add(new Document { Id = idx + 1, Path = doc, NumberOfOccurences = docIds[idx].Value });
                idx++;
            }
            Console.WriteLine(result);
            return result;
        } 

    }
}
