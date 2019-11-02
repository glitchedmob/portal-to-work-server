using PortalToWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalToWork.Repository
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> ListAsync();
        Task AddAsync(Device device);
    }
}
