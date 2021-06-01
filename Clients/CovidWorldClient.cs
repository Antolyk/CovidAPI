using CovidAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidAPI.Clients
{
    public class CovidWorldClient
    {
        private HttpClient _client;
        private static string _address;
        private static string _apikey;
        public CovidWorldClient()
        {
            _address = Constants.addressAll;
            _apikey = Constants.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", "d4a0c0f50cmsh6f70a22f2f64a0cp131f47jsn0f551a627d26");
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", "corona-virus-world-and-india-data.p.rapidapi.com");
        }
        public async Task<World_total> GetLatestStatisticByWorld()
        {
            var response = await _client.GetAsync("/api");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
           
            var result = JsonConvert.DeserializeObject<WorldStatistic>(content);

            return result.WorldStatisticModel;
        }
        public async Task<AllCountriesModels> GetTopStatistic(string description)
        {
            var response = await _client.GetAsync("/api");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var ListOfCountries = JsonConvert.DeserializeObject<AllCountriesModels>(content);
            var top10_most = new AllCountriesModels();
            var top10_less = new AllCountriesModels();                    
            if (description == "больше" || description == "Больше")
            {
                top10_most.countries_stat = new Countries_Stat[10];
                for (int i = 0; i < 10; i++)
                {
                    top10_most.countries_stat[i] = ListOfCountries.countries_stat[i];
                }
                return top10_most;
            }
            else if (description == "меньше" || description == "Меньше")
            {
                top10_less.countries_stat = new Countries_Stat[10];
                for (int i = 0; i < 10; i++)
                {
                    top10_less.countries_stat[i]=ListOfCountries.countries_stat[221 - i];
                }
                return top10_less;
            }
            else
            {
                return null;
            }
        }
    }
}
