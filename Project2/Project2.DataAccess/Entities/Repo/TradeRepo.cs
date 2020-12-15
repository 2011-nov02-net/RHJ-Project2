using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class TradeRepo : ITradeRepo
    {
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public TradeRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // not mapped
        public async Task<IEnumerable<AppTrade>> GetAllTrades()
        {
            using var context = new Project2Context(_contextOptions);
            var dbTrades = await context.Trades.ToListAsync();
            if (dbTrades == null) return null;
            var appTrades = dbTrades.Select(x => new AppTrade
            {
                TradeId = x.TradeId,
                OffererId = x.OffererId,
                BuyerId = x.BuyerId,
                IsClosed = x.IsClosed,
                TradeDate = x.TradeDate,
            });
            return appTrades;
        }

        // not mapped
        public async Task<AppTrade> GetOneTrade(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbTrade = await context.Trades.FirstOrDefaultAsync(x => x.TradeId == id);
            if (dbTrade == null) return null;
            var appTrade = new AppTrade
            {
                TradeId = dbTrade.TradeId,
                OffererId = dbTrade.OffererId,
                BuyerId = dbTrade.BuyerId,
                IsClosed = dbTrade.IsClosed,
                TradeDate = dbTrade.TradeDate,
            };
            return appTrade;
        }

        public async Task<AppTrade> AddOneTrade(AppTrade appTrade)
        {
            throw new NotImplementedException();
        }

        public void GetOneTradeDetail() 
        {

        }

        
    }
}
