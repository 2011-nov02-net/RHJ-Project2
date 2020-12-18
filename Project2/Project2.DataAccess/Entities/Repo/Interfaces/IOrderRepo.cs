using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<AppOrder>> GetAllOrders();
        Task<AppOrder> GetOneOrder(string id);
        Task<IEnumerable<AppPack>> GetOneOrderDetail(string id);

        Task<AppOrder> AddOneOrder(int quantity, AppOrder appOrder);

        Task<AppCard> GetCardFromApi(string cardId, string baseset);
        int GetRarity(string apiRarity);
    }
}
