using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Cosmos.Models;

namespace Cosmos.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        private Container _container;

        public CosmosDBService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(People item)
        {
            await this._container.CreateItemAsync<People>(item, new PartitionKey(item.Surname));
        }

        public async Task DeleteItemAsync(string id, string surname)
        {
            await this._container.DeleteItemAsync<People>(id, new PartitionKey(surname));
        }

        public async Task<People> GetItemAsync(string id, string surname)
        {
            try
            {
                ItemResponse<People> response = await this._container.ReadItemAsync<People>(id, new PartitionKey(surname));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<People>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<People>(new QueryDefinition(queryString));
            List<People> results = new List<People>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, People item)
        {
            await _container.UpsertItemAsync<People>(item, new PartitionKey (item.Surname));
        }
    }
}
