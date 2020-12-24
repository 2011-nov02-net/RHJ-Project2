using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DataAccess.Entities.Repo.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using PokemonTcgSdk;

namespace Project2.DataAccess.Entities.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Project2Context _context;
        public OrderRepo(Project2Context context)
        {
            _context  = context;
        }

        // not mapped
        public async Task<IEnumerable<AppOrder>> GetAllOrders()
        {
   
            var dbOrders = await _context.Orders.ToListAsync();
            if (dbOrders == null) return null;
            var AppOrders = dbOrders.Select(x => new AppOrder
            {
                OrderId = x.OrderId,
                OrdererId = x.UserId,
                Date = x.Date,
                Total = x.Total,
            });
            return AppOrders;

        }

        // not mapped
        public async Task<AppOrder> GetOneOrder(string id)
        {
     
            var dbOrder = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (dbOrder == null) return null;
            var AppOrder = new AppOrder
            {
                OrderId = dbOrder.OrderId,
                OrdererId = dbOrder.UserId,
                Date = dbOrder.Date,
                Total = dbOrder.Total,
            };
            return AppOrder;
        }

        // not mapped
        public async Task<IEnumerable<AppPack>> GetOneOrderDetail(string id)
        {
    
            var dbOrder = await _context.Orders.Include(x => x.OrderItems)
                                        .ThenInclude(x => x.Pack).FirstOrDefaultAsync(x => x.OrderId == id);
            if (dbOrder == null) return null;

            var appPacks = dbOrder.OrderItems.Select(x => new AppPack
            {

                PackId = x.PackId,
                Name = x.Pack.Name,
                Price = x.Pack.Price,
                DateReleased = x.Pack.DateReleased,
                PackQty = x.PackQty,
            });
            return appPacks;
        }

        // not mapped
        // handle duplicates outside
        // simple version that deals with one type of pack only
        public async Task<AppOrder> AddOneOrder(int quantity, AppOrder order)
        {
            // Order table
 
            var newOrder = new Order
            {
                OrderId = order.OrderId,
                UserId = order.OrdererId,
                Date = order.Date,
                Total = order.Total,
            };
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            // OrderItem table
            // order packA quan 2
            // order packB quan 3
            // for loop
            var newOrderItem = new OrderItem
            {
                OrderId = order.OrderId,
                PackId = order.PackId,
                PackQty = quantity,
            };
            await _context.OrderItems.AddAsync(newOrderItem);
            await _context.SaveChangesAsync();
            return order;
        }

        //Get card info from pokemon api
        public async Task<AppCard> GetCardFromApi(string cardId, string baseset)
        {
            var card = await PokemonTcgSdk.Card.FindAsync<Pokemon>("base" + baseset + "-" + cardId);

            int rarity = GetRarity(card.Card.Rarity);

            //initial rating and numOfRatings = 0
            var returnCard = new AppCard(1,1,rarity)
            {
                CardId = card.Card.Id,
                Name = card.Card.Name,
                Type = card.Card.Types.First(),
                Image = card.Card.ImageUrl
            };

            return returnCard;
        }

        //convert rarity string to int
        public int GetRarity(string apiRarity)
        {
            switch(apiRarity)
            {
                case "Common":
                    return 1;
                case "Uncommon":
                    return 2;
                case "Rare":
                    return 3;
                default:
                    return 1;
            }
        }
        public async Task<string> IdGen()
        {
            int lastOrderId = 0;
            var dbOrders = await _context.Orders.ToListAsync();
            if (dbOrders != null)
            {
                lastOrderId = dbOrders.Select(x => Convert.ToInt32(x.OrderId)).Max();
            }
            string newId = Convert.ToString(lastOrderId + 1);
            return newId;
        }
    }
}
