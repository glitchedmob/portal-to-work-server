using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalToWork.Models;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    public class AlgoliaController : ControllerBase
    {
        [HttpGet]
        [Route("refresh")]
        public async Task<ActionResult<string>> Refresh()
        {
            var jobs = await GetAllJobs();
            // var client = new SearchClient(
            //     Environment.GetEnvironmentVariable("ALGOLIA_APP_ID"),
            //     Environment.GetEnvironmentVariable("ALGOLIA_ADMIN_KEY")
            // );
            // var index = client.InitIndex("jobs");

            // var alljobs = index.ReplaceAllObjects();
            return "success";
        }

        private static async Task<IEnumerable<Job>> GetAllJobs()
        {
            using (var client = new HttpClient())
            {
                var token = Environment.GetEnvironmentVariable("WORKFORCE_API_KEY");
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("https://jobs.api.sgf.dev/api/job");
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Job>();
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<H4GApiRootObject>(content).data;
            }
        }
    }
}