using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalToWork.Data;
using PortalToWork.Models;

namespace PortalToWork.Repository
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Device>> ListAsync()
        {
            return await _context.Devices.ToListAsync();
        }
    }
}
