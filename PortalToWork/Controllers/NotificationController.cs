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
                return Ok();

            var algoliaJobs = _mapper.Map<List<AlgoliaJob>>(webhookRoot.jobs.data);

            index.SaveObjects(algoliaJobs);


            return Created(new Uri("https://hack4goodsgf.com"), algoliaJobs);
        }
    }
}