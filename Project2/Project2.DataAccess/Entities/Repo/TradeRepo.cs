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
        private readonly Project2Context _context;
        public TradeRepo( Project2Context context)
        {
            _context = context;
        }

        // not mapped
        public async Task<IEnumerable<AppTrade>> GetAllTrades()
        {
             
            var dbTrades = await _context.Trades.ToListAsync();
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
           
            var dbTrade = await _context.Trades.FirstOrDefaultAsync(x => x.TradeId == id);
            if (dbTrade == null) return null;
            var dbTradeDetail = await _context.TradeDetails.FirstOrDefaultAsync(x => x.TradeId == id);
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
             
            var dbTrade = new Trade
            {
                TradeId = appTrade.TradeId,
                OffererId = appTrade.Offerer.UserId,
                BuyerId = appTrade.Buyer.UserId,
                IsClosed = appTrade.IsClosed,
                TradeDate = appTrade.TradeDate,
            };
            await _context.Trades.AddAsync(dbTrade);
            await _context.SaveChangesAsync();

            var dbTradeDetail = new TradeDetail
            {
                TradeId = appTrade.TradeId,
                OfferCardId = appTrade.OfferCard.CardId,
                BuyerCardId = appTrade.BuyerCard.CardId,
            };
            await _context.TradeDetails.AddAsync(dbTradeDetail);
            await _context.SaveChangesAsync();

            return appTrade;
        }
        /// <summary>
        /// updates the db with passed AppTrade
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTrade"></param>
        /// <returns>True on success, false otherwise</returns>
        public async Task<bool> UpdateTradeById(string id, AppTrade updateTrade)
        {
            var dbTrade = await _context.Trades.FirstOrDefaultAsync(x => x.TradeId == id);
            var dbTradeDetail = await _context.TradeDetails.FirstOrDefaultAsync(x => x.TradeId == id);
            if (dbTrade == null)
                return false;
            else if (dbTradeDetail == null)
                return false;
            else
            {
                dbTrade.TradeId = updateTrade.TradeId;
                dbTrade.BuyerId = updateTrade.BuyerId;
                dbTrade.OffererId = updateTrade.OffererId;
                dbTrade.IsClosed = updateTrade.IsClosed;
                dbTrade.TradeDate = updateTrade.TradeDate;
                //tradedetail
                dbTradeDetail.OfferCardId = updateTrade.OfferCard.CardId;
                dbTradeDetail.BuyerCardId = updateTrade.BuyerCard.CardId;
                return true;
            }
        }
    }
}
