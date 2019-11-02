using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalToWork.Models.H4G
{
    public class H4GLocation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
