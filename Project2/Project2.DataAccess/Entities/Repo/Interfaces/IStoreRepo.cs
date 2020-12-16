using Project2.Domain;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface IStoreRepo
    {
        Task<IEnumerable<AppStoreItem>> GetAllStoreItems();
        Task<AppStoreItem> GetStoreItemById(string id);
        Task<bool> UpdateStoreItemById(string id, string option, int amount);
    }
}