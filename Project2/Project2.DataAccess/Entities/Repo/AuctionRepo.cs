using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class AuctionRepo : IAuctionRepo
    {
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public AuctionRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        //Get all auctions
        public async Task<IEnumerable<AppAuction>> GetAllAuctions()
        {
            using var context = new Project2Context(_contextOptions);

            var dbAuctions = await context.Auctions.ToListAsync();

            if (dbAuctions == null)
                return null;

            var appAuctions = DomainDataMapper.GetAllAuctions(dbAuctions);

            return appAuctions;
        }

        //Get auction by id
        public async Task<AppAuction> GetAuctionById(string id)
        {
            using var context = new Project2Context(_contextOptions);

            var dbAuctions = await context.Auctions.FirstOrDefaultAsync(x => x.AuctionId == id);
            if (dbAuctions == null) 
                return null;

            var appAuction = DomainDataMapper.GetAuction(dbAuctions);
            return appAuction;
        }

        //get auction details by id
        public async Task<AppAuction> GetAuctionDetailById(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbDetail = await context.AuctionDetails.FirstOrDefaultAsync(x => x.AuctionId == id);
            if (dbDetail == null)
                return null;

            var appAuctionDetail = DomainDataMapper.GetAuctionDetail(dbDetail);
            return appAuctionDetail;
        }

        //Create an auction
        public async Task<bool> CreateAuction(AppAuction auction)
        {
            using var context = new Project2Context(_contextOptions);
            if (auction == null)
            {
                return false;
            }
            else
            {
                //create db objects
                var dbAuction = new Auction
                {
                    AuctionId = auction.AuctionId,
                    SellerId = auction.SellerId,
                    BuyerId = auction.BuyerId,
                    CardId = auction.CardId,
                    PriceSold = auction.PriceSold,
                    SellDate = auction.SellDate
                };
                var dbAuctionDetail = new AuctionDetail
                {
                    AuctionId = auction.AuctionId,
                    PriceListed = auction.PriceListed,
                    BuyoutPrice = auction.BuyoutPrice,
                    NumberBids = auction.NumberBids,
                    SellType = auction.SellType,
                    ExpDate = auction.ExpDate
                };

                //Add to db
                try
                {
                    await context.Auctions.AddAsync(dbAuction);
                    await context.AuctionDetails.AddAsync(dbAuctionDetail);
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Creating Auction: " + e);
                }
            }

            return false;
        }

        //Update auction by id
        public async Task<bool> UpdateAuction(string id, AppAuction auction)
        {
            using var context = new Project2Context(_contextOptions);
            var updateAuction = await context.Auctions.Where(x => x.AuctionId == id).FirstAsync();
            if (auction == null)
            {
                return false;
            }
            else
            {
                try
                {
                    //update db record
                    updateAuction.SellerId = auction.SellerId;
                    updateAuction.BuyerId = auction.BuyerId;
                    updateAuction.CardId = auction.CardId;
                    updateAuction.PriceSold = auction.PriceSold;
                    updateAuction.SellDate = auction.SellDate;
                    
                    //save updated record
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Updating Auction: " + e);
                }
            }

            return false;
        }

        //Update auction detail by id
        public async Task<bool> UpdateAuctionDetail(string id, AppAuction auction)
        {
            using var context = new Project2Context(_contextOptions);
            var updateAuctionDetail = await context.AuctionDetails.Where(x => x.AuctionId == id).FirstAsync();
            if (auction == null)
            {
                return false;
            }
            else
            {
                try
                {
                    //update db record
                    updateAuctionDetail.PriceListed = auction.PriceListed;
                    updateAuctionDetail.BuyoutPrice = auction.BuyoutPrice;
                    updateAuctionDetail.NumberBids = auction.NumberBids;
                    updateAuctionDetail.SellType = auction.SellType;
                    updateAuctionDetail.ExpDate = auction.ExpDate;

                    //save updated record
                    await context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Updating Auction Detail: " + e);
                }
            }

            return false;
        }
    }
}
