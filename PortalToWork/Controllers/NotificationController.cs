using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Data;
using PortalToWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using PortalToWork.Models.H4G;
using AutoMapper;
using PortalToWork.Models.Algolia;
using Algolia.Search.Clients;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        protected readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NotificationController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        [HttpGet("{playerId}")]
        public async Task<List<Notification>> GetNotifications(string playerId)
        {
            var notifications = await _context.Notifications.Where(n => n.PlayerId == playerId).ToListAsync();  

            return notifications;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        [HttpPost]
        [Route("new-jobs-webhook")]
        public async Task<IActionResult> NewJobsWebhook(WebhookRoot webhookRoot)
        {
            var client = new SearchClient(
                Environment.GetEnvironmentVariable("ALGOLIA_APP_ID"),
                Environment.GetEnvironmentVariable("ALGOLIA_ADMIN_KEY")
            );
            var index = client.InitIndex("jobs");

            if (webhookRoot.jobs == null)
            {
                return Ok();
            }

            var algoliaJobs = _mapper.Map<List<AlgoliaJob>>(webhookRoot.jobs.data);

            index.SaveObjects(algoliaJobs);

            //send notifications
            var playerIds = await _context.Devices.Select(d => d.PlayerID).ToListAsync();

            var result = await SendNotificationsToPlayerIDs(playerIds, webhookRoot.jobs.data);


            return Created(new Uri("https://hack4goodsgf.com"), algoliaJobs);
        }

        private async Task<string> SendNotificationsToPlayerIDs(List<string> playerIds, List<H4GJob> jobs)
        {
            var playerIdsString = "";

            var i = 0;
            foreach(var playerId in playerIds)
            {
                playerIdsString += $"\"{playerId}\"";
                if(i < playerIds.Count - 1)
                {
                    playerIdsString += ", ";
                }

                i++;
            }

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonPayload = $@"{{
                    ""app_id"": ""{Environment.GetEnvironmentVariable("ONESIGNAL_APP_ID")}"",
                    ""contents"": {{
                        ""en"": ""New job posting: {jobs[0].title}""
                    }},
                    ""include_player_ids"": [{playerIdsString}],
                    ""url"": ""https://portal-to-work.netlify.com/#/app/jobs/{jobs[0].id}""
                }}";

                var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", stringContent);


                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}