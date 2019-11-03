using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PortalToWork.Models.Algolia
{
    public class AlgoliaJob
    {
        [JsonProperty("objectID")]
        public string ObjectId { get; set; }
        public string date_posted { get; set; }
        public string date_updated { get; set; }
        public string date_expires { get; set; }
        public int employer_id { get; set; }
        public Employer employer { get; set; }
        public LocationList locations { get; set; }
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
        [JsonProperty("_geoloc")]
        //public GeoData geodata { get; set; }
        public List<GeoData> geodata { get; set; }
    }
}