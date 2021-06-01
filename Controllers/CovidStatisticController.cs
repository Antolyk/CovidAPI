using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CovidApi.Clients;
using CovidAPI.Clients;
using CovidAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CovidAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidStatisticController : ControllerBase
    {
        private readonly ILogger<CovidStatisticController> _logger;
        private readonly CovidClient _covidClient;
        private readonly CovidWorldClient _covidWorldClient;
        private readonly ContinentClient _continentClient;
        private readonly IDynamoDbClient _dynamoDBClient;
        public CovidStatisticController(ILogger<CovidStatisticController> logger, CovidClient covidClient, CovidWorldClient covidWorldClient, ContinentClient continentClient, IDynamoDbClient dynamoDBClient)
        {
            _logger = logger;
            _covidClient = covidClient;
            _covidWorldClient = covidWorldClient;
            _dynamoDBClient = dynamoDBClient;
            _continentClient = continentClient;
        }
        [HttpGet("country")]
        public async Task<Statistic> GetStatisticByCountry([FromQuery] Parameters parameters)
        {
            var statistic = await _covidClient.GetLatestStatisticByCountry(parameters.country);
            return statistic;
        }
        [HttpGet("all")]
        public async Task<World_total> GetStatisticByWorld()
        {
            var statistic = await _covidWorldClient.GetLatestStatisticByWorld();
            return statistic;
        }
        [HttpGet("controlByNumber")]
        public async Task<string> GetComparisonWithANumber([FromQuery] Parameters parameters)
        {
            var answer = await _covidClient.GetComparisonWithANumberByCountry(parameters.country, parameters.number, parameters.description);
            return answer;
        }
        [HttpGet("top")]
        public async Task<AllCountriesModels> GetTop([FromQuery] Parameters parameters)
        {
           var result = await _covidWorldClient.GetTopStatistic(parameters.description);
            return result;

        }
        [HttpGet("continent")]
        public async Task<ContinentModel> GetContinentStatistic([FromQuery] Parameters parameters)
        {
            var result = await _continentClient.GetStatisticByContinent(parameters.continent);
            return result;

        }      
        [HttpGet("db")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotifications([FromQuery] int id)
        {
            var result = await _dynamoDBClient.GetDataByUser(id);
            if (result == null)
                return NotFound("Record doesn't exist in database");            
            return Ok(result);
        }      
        [HttpPost("addToDb")]
        public async Task<IActionResult> AddToNotifications([FromBody] Notifications notifications)
        {
            var data = new Notifications()
            {
                ID = notifications.ID,
                Country = notifications.Country,
                UserID = notifications.UserID
            };
            var result = await _dynamoDBClient.PostDataToDb(data);
            if(result == false)
            {
                return BadRequest("Cannot insert value to DB");
            }
            return Ok("Valuse has been successfully added to DB");
        }
    }
}
