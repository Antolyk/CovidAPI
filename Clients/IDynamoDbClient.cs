using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidAPI.Models;

namespace CovidApi.Clients
{
    public interface IDynamoDbClient
    {
        public Task<Notifications> GetDataByUser(int id);
        public Task<bool> PostDataToDb(Notifications data);
        public Task UpdateDataIntoDb();
        public Task Delete();
        public Task<List<Notifications>> GetAll();
    }
}