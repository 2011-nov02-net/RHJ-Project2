using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class OrderRepo// : IOrderRepo
    {
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public OrderRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // not mapped
        /*public async Task<IEnumerable<AppOrder>> GetAllOrders()
        { 
            using var context = new Project2Context(_contextOptions);
            var dbOrders = await context.DOrders.ToListAsync();
            if (dbOrders == null) return null;
            var AppOrders = dbOrders.Select(x => new AppOrder(x.OrderId, x.UserId, x.Date, x.Total));
            return AppOrders;

        }

        // not mapped
        public async Task<AppOrder> GetOneOrder(string id)
        { 
            using var context = new Project2Context(_contextOptions);
            var dbOrder = await context.DOrders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (dbOrder == null) return null;
            var AppOrder = new AppOrder(dbOrder.OrderId, dbOrder.UserId, dbOrder.Date, dbOrder.Total);
            return AppOrder;
        }*/

        public Task<Order> GetOneOrderDetail(string id)
        {
            throw new NotImplementedException();
        }
    }
}
