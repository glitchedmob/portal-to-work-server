using System;

namespace PortalToWork.Models
{
    public class Employer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int naics { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}