using System.Collections.Generic;
using PortalToWork.Models.H4G;

namespace PortalToWork.Models
{
    public class WebhookRoot
    {
        public H4GApiRootObject jobs { get; set; }
        public Dictionary<string, string> events { get; set; }
    }
}