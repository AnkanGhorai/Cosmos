using Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmos.Services
{
    public interface IPlacesDBServices
    {
        Task<IEnumerable<Places>> GetItemsAsync(string query);
        Task<Places> GetItemAsync(string id, string surname);
        Task AddItemAsync(Places item);
        Task UpdateItemAsync(string id, Places item);
        Task DeleteItemAsync(string id, string state);
    }
}
