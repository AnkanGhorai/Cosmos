using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Models;

namespace Cosmos.Services
{
    public interface IProductsDBServices
    {
        Task<IEnumerable<Products>> GetItemsAsync(string query);
        Task<Products> GetItemAsync(string id, string type);
        Task AddItemAsync(Products item);
        Task UpdateItemAsync(string id, Products products);
        Task DeleteItemAsync(string id, string type);
    }
}
