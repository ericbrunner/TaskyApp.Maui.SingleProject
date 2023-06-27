using System.Collections.Generic;
using System.Threading.Tasks;
using TaskyApp.Contracts.Models;

namespace TaskyApp.Contracts
{
    public interface ITodoStore : IDataStore<Todo> { }
    public interface IDataStore<TEntity>
    {
        Task<bool> AddItemAsync(TEntity item);
        Task<bool> UpdateItemAsync(TEntity item);
        Task<bool> DeleteItemAsync(long id);
        Task<TEntity> GetItemAsync(long id);
        Task<IEnumerable<TEntity>> GetItemsAsync(bool forceRefresh = false);
    }
}
