using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidAPI.Models
{
    public class WorldStatistic
    {
        [JsonProperty(PropertyName = "world_total")]
        public World_total WorldStatisticModel { get; set; }
    }
    public class World_total
    {
        public string Total_cases { get; set; }
        public string New_cases { get; set; }
        public string Total_deaths { get; set; }
        public string New_deaths { get; set; }
        public string Total_recovered { get; set; }
        public string Active_cases { get; set; }
        public string Serious_critical { get; set; }

    }
}
