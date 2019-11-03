using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Data;
using PortalToWork.Models;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeviceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Device device)
        {
            var existingDevices = await _context.Devices.Where(d => d.PlayerID == device.PlayerID).ToListAsync();

            if (existingDevices.Any())
            {
                _context.Devices.RemoveRange(existingDevices);
                await _context.SaveChangesAsync();
            }

            await _context.AddAsync(device);

            return Created(new Uri("https://hack4goodsgf.com"), device);
        }
    }
}