using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidAPI.Models
{
    public class ContinentModel
    {       
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string continent { get; set; }
        public string totalCases { get; set; }       
        public string totalDeaths { get; set; }
        public string totalRecovered { get; set; }
    }
}