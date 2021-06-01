using CovidAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace CovidAPI.Clients
{
    public class CovidClient
    {
        private HttpClient _client;
        private static string _address;
        private static string _apikey;
        public CovidClient()
        {
            _address = Constants.addressCountry;
            _apikey = Constants.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", "d4a0c0f50cmsh6f70a22f2f64a0cp131f47jsn0f551a627d26");
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-19-data.p.rapidapi.com");
        }
        public async Task<Statistic> GetLatestStatisticByCountry(string country)
        {
            var response = await _client.GetAsync($"/country?name={country}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<IEnumerable<Statistic>>(content);

            return result.First();
        }
        public async Task<string> GetComparisonWithANumberByCountry(string country, int number, string descript)
        {
            var response = await _client.GetAsync($"/country?name={country}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var reg = new Regex(@"""confirmed"":\d+");
            string conf = "";
            MatchCollection matches = reg.Matches(content);
            foreach (Match match in matches)
                conf = match.Value;
            int value = 0;
            var regex = new Regex(@"\d+");
            MatchCollection matchesNumber = regex.Matches(conf);
            foreach (Match match in matchesNumber)
                value = Int32.Parse(match.Value);
            if(descript == "больше" || descript == "Больше")
            {
                if (value > number)
                {
                    return $"Заболевших в {country} стало больше чем {number}, а именно {value}, что на {value - number} больше";
                }
                else
                    return $"Заболевших в {country} меньше чем {number} на {number - value}";
            }else if(descript == "меньше" || descript == "Меньше")
            {
                if (value < number)
                {
                    return $"Заболевших в {country} по прежнему меньше чем {number}. Ваше значение больше на {number - value}";
                }
                else
                    return $"Заболевших в {country} больше чем {number} на {value - number}";
            }
            else
            {
                return "Не верный запрос";
            }                    
        }
    }
}
