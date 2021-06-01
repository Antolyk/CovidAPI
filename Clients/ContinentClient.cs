using CovidAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidAPI.Clients
{
    public class ContinentClient
    {
        private HttpClient _client;
        private static string _address;
        private static string _apikey;
        public ContinentClient()
        {
            _address = Constants.addressContinent;
            _apikey = Constants.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", "d4a0c0f50cmsh6f70a22f2f64a0cp131f47jsn0f551a627d26");
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-19-coronavirus-statistics2.p.rapidapi.com");
        }
        public async Task<ContinentModel> GetStatisticByContinent(string continent)
        {
            var response = await _client.GetAsync($"/continentData");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ContinentModel>(content);         
            if (continent == "North America" || continent == "north america")
            {
                var northAmericaStat = new ContinentModel();
                northAmericaStat.result = new Result[1];
                northAmericaStat.result[0] = result.result[0];
                return northAmericaStat;
            }
           else if (continent == "Asia" || continent == "asia")
            {
                var asiaStat = new ContinentModel();
                asiaStat.result = new Result[1];
                asiaStat.result[0] = result.result[1];
                return asiaStat;
            }
           else if (continent == "South America" || continent == "sorth america")
            {
                var southAmericaStat = new ContinentModel();
                southAmericaStat.result = new Result[1];
                southAmericaStat.result[0] = result.result[2];
                return southAmericaStat;
            }
            else if (continent == "Europe" || continent == "europe")
            {
                var europeStat = new ContinentModel();
                europeStat.result = new Result[1];
                europeStat.result[0] = result.result[3];
                return europeStat;
            }
            else if (continent == "Africa" || continent == "africa")
            {
                var africaStat = new ContinentModel();
                africaStat.result = new Result[1];
                africaStat.result[0] = result.result[4];
                return africaStat;
            }
            else if (continent == "Oceania" || continent == "oceania")
            {
                var oceaniaStat = new ContinentModel();
                oceaniaStat.result = new Result[1];
                oceaniaStat.result[0] = result.result[5];
                return oceaniaStat;
            }
            else
            {
                return null;
            }           
        }
    }
}
