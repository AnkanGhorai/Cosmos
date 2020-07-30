using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Cosmos.Models;

namespace Cosmos.Services
{
    public class ProductsDBServices: IProductsDBServices
    {
        private Container _container;
        public ProductsDBServices(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }


        public async Task AddItemAsync(Products products)
        {
            await this._container.CreateItemAsync<Products>(products, new PartitionKey(products.Type));
        }

        public async Task DeleteItemAsync(string id, string type)
        {
            await _container.DeleteItemAsync<Products>(id, new PartitionKey(type));
        }

        public async Task<Products> GetItemAsync(string id, string type)
        {
            try
            {
                ItemResponse<Products> response = await this._container.ReadItemAsync<Products>(id, new PartitionKey(type));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Products>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Products>(new QueryDefinition(queryString));
            List<Products> results = new List<Products>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Products products)
        {
            await _container.UpsertItemAsync<Products>(products, new PartitionKey(products.Type));
        }
    }
}
