using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Models;

namespace Cosmos.Services
{
    public interface ICosmosDBService
    {
        Task<IEnumerable<People>> GetItemsAsync(string query);
        Task<People> GetItemAsync(string id, string surname);
        Task AddItemAsync(People item);
        Task UpdateItemAsync(string id, People item);
        Task DeleteItemAsync(string id, string surname);
    }
}
