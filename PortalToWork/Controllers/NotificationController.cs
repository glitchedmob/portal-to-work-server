using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Data;
using PortalToWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        protected readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
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
    }
}