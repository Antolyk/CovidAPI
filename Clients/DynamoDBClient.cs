using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CovidApi.Clients;
using CovidAPI.Clients;
using CovidAPI.Extensions;
using CovidAPI.Models;

namespace CovidAPI.Clients
{
    public class DynamoDBClient : IDynamoDbClient
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDB;
        public DynamoDBClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
            _tableName = Constants.TableName;
        }
        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task<List<Notifications>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Notifications> GetDataByUser(int id)
        {
            var item = new GetItemRequest()
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                   {"ID",new AttributeValue{ N = $"{id}"} }
                }
            };
            var response = await _dynamoDB.GetItemAsync(item);
            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.ToClass<Notifications>();
            return result;
        }

        public async Task<bool> PostDataToDb(Notifications data)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"ID", new AttributeValue{N = $"{data.ID}"} },
                    {"Country", new AttributeValue{S = data.Country} },
                    {"UserID", new AttributeValue{N = $"{data.UserID}"} }
                }
            };
            try
            {
                var response = await _dynamoDB.PutItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                Console.WriteLine("Here is problem");
                return false;
            }
        }

        public Task UpdateDataIntoDb()
        {
            throw new NotImplementedException();
        }
    }
}
