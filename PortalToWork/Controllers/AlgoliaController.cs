using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalToWork.Models;
using PortalToWork.Models.Algolia;
using PortalToWork.Models.H4G;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    public class AlgoliaController : ControllerBase
    {
        private readonly IMapper _mapper;

        public AlgoliaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("refresh")]
        public async Task<ActionResult<string>> Refresh()
        {
            var h4GJobs = await GetAllJobs();
            
            var client = new SearchClient(
                Environment.GetEnvironmentVariable("ALGOLIA_APP_ID"),
                Environment.GetEnvironmentVariable("ALGOLIA_ADMIN_KEY")
            );
            var index = client.InitIndex("jobs");

            try
            {
                var algoliaJobs = _mapper.Map<List<AlgoliaJob>>(h4GJobs);

                index.ReplaceAllObjects(algoliaJobs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            // var alljobs = index.ReplaceAllObjects();
            return "success";
        }

        private static async Task<IEnumerable<H4GJob>> GetAllJobs()
        {
            using (var client = new HttpClient())
            {
                var token = Environment.GetEnvironmentVariable("WORKFORCE_API_KEY");
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("https://jobs.api.sgf.dev/api/job");
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<H4GJob>();
                }

                var content = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<H4GApiRootObject>(content).data;
                return res;
            }
        }
    }
}