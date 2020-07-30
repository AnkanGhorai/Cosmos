using Cosmos.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmos.Services
{
    public class PlacesDBService : IPlacesDBServices
    {
        private Container _container;
        public PlacesDBService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Places item)
        {
            await this._container.CreateItemAsync<Places>(item, new PartitionKey(item.State));
        }

        public async Task DeleteItemAsync(string id, string state)
        {
            await _container.DeleteItemAsync<Places>(id, new PartitionKey(state));
        }

        public async Task<Places> GetItemAsync(string id, string state)
        {
            try
            {
                ItemResponse<Places> response = await this._container.ReadItemAsync<Places>(id, new PartitionKey(state));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Places>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Places>(new QueryDefinition(queryString));
            List<Places> results = new List<Places>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Places item)
        {
            await _container.UpsertItemAsync<Places>(item, new PartitionKey(item.State));
        }
    }
}
