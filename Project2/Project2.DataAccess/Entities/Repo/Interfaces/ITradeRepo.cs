using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo.Interfaces
{
    public interface ITradeRepo
    {
        Task<IEnumerable<AppTrade>> GetAllTrades();
        Task<AppTrade> GetOneTrade(string id);
        Task<AppTrade> AddOneTrade(AppTrade appTrade);

        Task<bool> UpdateTradeById(string id, AppTrade updateTrade);

        Task<string> IdGen();
    }
}
