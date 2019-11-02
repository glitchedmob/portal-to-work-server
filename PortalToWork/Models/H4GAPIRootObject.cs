using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }

    public class Locations
    {
        public List<Location> data { get; set; }
    }

    public class Job
    {
        public int id { get; set; }
        public string date_posted { get; set; }
        public string date_updated { get; set; }
        public string date_expires { get; set; }
        public int employer_id { get; set; }
        public Employer employer { get; set; }
        public Locations locations { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string job_type { get; set; }
        public int job_id { get; set; }
        public string pay_rate { get; set; }
        public string req_education { get; set; }
        public string data_source { get; set; }
        public string data_site { get; set; }
        public string url { get; set; }
        public int fake { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class H4GAPIRootObject
    {
        public List<Job> data { get; set; }
    }
 
}
