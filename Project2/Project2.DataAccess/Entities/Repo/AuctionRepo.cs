using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class AuctionRepo
    {
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public AuctionRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        //Get all auctions
        /*public async Task<IEnumerable<AppAuction>> GetAllAuctions()
        {
            using var context = new Project2Context(_contextOptions);

            var dbAuctions = await context.Auctions.ToListAsync();

            if (dbAuctions == null)
                return null;

            var appAuction = DomainDataMapper.GetAllAuctions(dbAuctions);

            return appPacks;
        }*/

        //Create an auction

        //Get auction by id

        //Update auction by id

        //get auction details by id
    }
}
