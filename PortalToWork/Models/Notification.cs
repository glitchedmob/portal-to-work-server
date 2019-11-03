using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalToWork.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public int JobId { get; set; }
    }
}
