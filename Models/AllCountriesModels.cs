using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidAPI.Models
{

    public class AllCountriesModels
    {
        public Countries_Stat[] countries_stat { get; set; }
    }

    public class Countries_Stat
    {
        public string country_name { get; set; }
        public string cases { get; set; }
        public string deaths { get; set; }
        public string total_recovered { get; set; }
        public string serious_critical { get; set; }
        public string active_cases { get; set; }      
    }

}
