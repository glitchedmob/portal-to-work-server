using System;

namespace PortalToWork.Models
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public GeoData _geoloc { get; set; }
    }
}