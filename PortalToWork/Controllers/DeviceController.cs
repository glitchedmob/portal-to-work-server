using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalToWork.Models;
using PortalToWork.Repository;

namespace PortalToWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceService)
        {
            _deviceRepository = deviceService;
        }

        [HttpGet]
        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            var devices = await _deviceRepository.ListAsync();
            return devices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Device device)
        {
            await _deviceRepository.AddAsync(device);

            return Created(new Uri("https://hack4goodsgf.com"), device);
        }
    }
}