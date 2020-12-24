using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo.Interfaces
{
    public interface IAuctionRepo
    {
        Task<IEnumerable<AppAuction>> GetAllAuctions();
        Task<AppAuction> GetAuctionById(string id);
        Task<bool> CreateAuction(AppAuction auction);
        Task<bool> UpdateAuction(string id, AppAuction auction);
        Task<string> IdGen();
    }
}
