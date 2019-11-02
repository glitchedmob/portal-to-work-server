using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Algolia.Search.Clients;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            /*
             * get in the data
             * loop through the Devices
             *     compare location in a 30mile radius
             *         send notifs to player ID
            */

            //DBshit = connect to db and getit
            //foreach (Devices device in DBshit)
            //{

            //}

            //I don't fuggin know where to out this
            SearchClient client = new SearchClient(Environment.GetEnvironmentVariable("ALGOLIA_APP_ID"), Environment.GetEnvironmentVariable("ALGOLIA_ADMIN_KEY"));
            SearchIndex index = client.InitIndex("jobs");

            var alljobs = 

            index.ReplaceAllObjects(
                value.data
            );


        }

        private static void GetAllJobs()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://jobs.api.sgf.dev/api/job");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("?api_token=" + Environment.GetEnvironmentVariable("WORKFORCE_API_KEY")).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }



            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
