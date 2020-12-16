using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DataAccess.Entities.Repo.Interfaces;

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
        /// <summary>
        /// creates a new trade from the Trade and TradeDetail tables
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns an AppTrade for error handling</returns>
        public async Task<AppTrade> GetOneTrade(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbTrade = await context.Trades.FirstOrDefaultAsync(x => x.TradeId == id);
            if (dbTrade == null) return null;
            var dbTradeDetail = await context.TradeDetails.FirstOrDefaultAsync(x => x.TradeId == id);
            if (dbTradeDetail == null) return null;

            var appTrade = new AppTrade
            {
                TradeId = dbTrade.TradeId,
                OffererId = dbTrade.OffererId,
                BuyerId = dbTrade.BuyerId,
                IsClosed = dbTrade.IsClosed,
                TradeDate = dbTrade.TradeDate,
                OfferCardId = dbTradeDetail.OfferCardId,
                BuyerCardId = dbTradeDetail.BuyerCardId
            };
            return appTrade;
        }
        /// <summary>
        /// Enter a Trade into the db
        /// </summary>
        /// <param name="appTrade"></param>
        /// <returns>returns the trade that was entered for error handling</returns>
        public async Task<AppTrade> AddOneTrade(AppTrade appTrade)
        {
            using var context = new Project2Context(_contextOptions);
            var dbTrade = new Trade
            {
                TradeId = appTrade.TradeId,
                OffererId = appTrade.Offerer.UserId,
                BuyerId = appTrade.Buyer.UserId,
                IsClosed = appTrade.IsClosed,
                TradeDate = appTrade.TradeDate,
            };
            await context.Trades.AddAsync(dbTrade);
            await context.SaveChangesAsync();

            var dbTradeDetail = new TradeDetail
            {
                TradeId = appTrade.TradeId,
                OfferCardId = appTrade.OfferCard.CardId,
                BuyerCardId = appTrade.BuyerCard.CardId,
            };
            await context.TradeDetails.AddAsync(dbTradeDetail);
            await context.SaveChangesAsync();

            return appTrade;
        }
    }
}
