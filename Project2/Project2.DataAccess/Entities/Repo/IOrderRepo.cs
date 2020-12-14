using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface IOrderRepo
    {
        Task<IEnumerable<AppOrder>> GetAllOrders();
        Task<AppOrder> GetOneOrder(string id);
        Task<Order> GetOneOrderDetail(string id);
    }
}
