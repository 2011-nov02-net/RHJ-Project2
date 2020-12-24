using Microsoft.EntityFrameworkCore;
using Project2.DataAccess.Entities.Repo.Interfaces;
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
        private readonly Project2Context _context ;
        private readonly IUserRepo _userRepo;
        private readonly ICardRepo _cardRepo;
        public AuctionRepo(Project2Context context , IUserRepo userRepo, ICardRepo cardRepo)
        {
            _cardRepo = cardRepo;
            _userRepo = userRepo;
            _context  = context ;
        }

        //Get all auctions
        public async Task<IEnumerable<AppAuction>> GetAllAuctions()
        {       
            var dbAuctions = await _context.Auctions.Include(x=>x.AuctionDetail).ToListAsync();
            
            if (dbAuctions == null)
                return null;

            var appAuctions = new List<AppAuction>();

            foreach (var dbAuction in dbAuctions)
            {
                var appAuction = new AppAuction()
                {
                    AuctionId = dbAuction.AuctionId,
                    SellerId = dbAuction.SellerId,
                    BuyerId = dbAuction.BuyerId,
                    CardId = dbAuction.CardId,
                    PriceSold = (double)dbAuction.PriceSold,
                    SellDate = (DateTime)dbAuction.SellDate,
                    PriceListed = dbAuction.AuctionDetail.PriceListed,
                    BuyoutPrice = dbAuction.AuctionDetail.BuyoutPrice,
                    NumberBids = dbAuction.AuctionDetail.NumberBids,
                    SellType = dbAuction.AuctionDetail.SellType,
                    ExpDate = dbAuction.AuctionDetail.ExpDate
                };

                //check if a buyer exists and set Buyer and Seller for Expiration check
                if (appAuction.BuyerId != "" && appAuction.BuyerId != null) 
                    appAuction.Buyer = await _userRepo.GetOneUser(appAuction.BuyerId);
                appAuction.Seller = await _userRepo.GetOneUser(appAuction.SellerId);
                appAuction.Card = await _cardRepo.GetOneCard(appAuction.CardId);

<<<<<<< HEAD
                //updates appAuction if UtcNow > ExpDate
                /*if(appAuction.Expired())
                {
                    await _userRepo.UpdateUserById(appAuction.BuyerId, appAuction.Buyer);
                    await _userRepo.UpdateUserById(appAuction.SellerId, appAuction.Seller);
                }*/
=======
                //updates appAuction and users if UtcNow > ExpDate
                if(appAuction.Expired())
                {
                    await _userRepo.UpdateUserById(appAuction.BuyerId, appAuction.Buyer);
                    await _userRepo.UpdateUserById(appAuction.SellerId, appAuction.Seller);
                    await UpdateAuction(appAuction.AuctionId,appAuction);
                    await _context.SaveChangesAsync();
                }
>>>>>>> 4ba751c9a7afb1e1d100ca4381eb9a386c6df918
                appAuctions.Add(appAuction);
            }

            return appAuctions;
        }

        //Get auction by id
        public async Task<AppAuction> GetAuctionById(string id)
        {
     
            var dbAuction = await _context.Auctions.Include(x => x.AuctionDetail).FirstOrDefaultAsync(x => x.AuctionId == id);
            if (dbAuction == null) 
                return null;
            
            var appAuction = new AppAuction()
            {
                AuctionId = dbAuction.AuctionId,
                SellerId = dbAuction.SellerId,
                BuyerId = dbAuction.BuyerId,
                CardId = dbAuction.CardId,
                PriceSold = (double)dbAuction.PriceSold,
                SellDate = (DateTime)dbAuction.SellDate,
                //details
                PriceListed = dbAuction.AuctionDetail.PriceListed,
                BuyoutPrice = dbAuction.AuctionDetail.BuyoutPrice,
                NumberBids = dbAuction.AuctionDetail.NumberBids,
                SellType = dbAuction.AuctionDetail.SellType,
                ExpDate = dbAuction.AuctionDetail.ExpDate
            };
            //check if a buyer exists and set Buyer and Seller for Expiration check
            if (appAuction.BuyerId != "" && appAuction.BuyerId != null)
                appAuction.Buyer = await _userRepo.GetOneUser(appAuction.BuyerId);
            appAuction.Seller = await _userRepo.GetOneUser(appAuction.SellerId);
            appAuction.Card = await _cardRepo.GetOneCard(appAuction.CardId);

            //updates appAuction and users if UtcNow > ExpDate
            if (appAuction.Expired())
            {
                await _userRepo.UpdateUserById(appAuction.BuyerId, appAuction.Buyer);
                await _userRepo.UpdateUserById(appAuction.SellerId, appAuction.Seller);
                await UpdateAuction(appAuction.AuctionId, appAuction);
                await _context.SaveChangesAsync();
            }

            return appAuction;
        }

        //Create an auction
        public async Task<bool> CreateAuction(AppAuction auction)
        {
 
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
                    await _context.Auctions.AddAsync(dbAuction);
                    await _context.AuctionDetails.AddAsync(dbAuctionDetail);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Creating Auction: " + e);
                }
            }

            return false;
        }

        //Perform any business logic on passed AppAuction, then enter into db TODO: move to controller/domain
        public async Task<bool> UpdateAuction(string id, AppAuction auction)
        {
            
            var updateAuction = await _context.Auctions.Include(x => x.AuctionDetail).Where(x => x.AuctionId == id).FirstAsync();
            if (auction == null)
                return false;

            if (auction.BuyerId != "" && auction.BuyerId != null)
                auction.Buyer = await _userRepo.GetOneUser(auction.BuyerId);
            auction.Seller = await _userRepo.GetOneUser(auction.SellerId);
            auction.Card = await _cardRepo.GetOneCard(auction.CardId);

            //if there is a Price sold, the auction was bought out
            if (/*auction.SellType != "Buyout" &&*/ auction.PriceSold > 0 && updateAuction.PriceSold != auction.PriceSold)
            {
                auction.BuyOut();
            }
            //if the auction had a new priceListed it has been bid on
            else if (auction.PriceListed != updateAuction.AuctionDetail.PriceListed)
            {
                double bid = auction.PriceListed - updateAuction.AuctionDetail.PriceListed;
                auction.NewBid(DateTime.UtcNow, bid);
            }
            
            try
            {
                //update db record
                updateAuction.SellerId = auction.SellerId;
                updateAuction.BuyerId = auction.BuyerId;
                updateAuction.CardId = auction.CardId;
                updateAuction.PriceSold = auction.PriceSold;
                updateAuction.SellDate = auction.SellDate;
                //update details
                updateAuction.AuctionDetail.PriceListed = auction.PriceListed;
                updateAuction.AuctionDetail.BuyoutPrice = auction.BuyoutPrice;
                updateAuction.AuctionDetail.NumberBids = auction.NumberBids;
                updateAuction.AuctionDetail.SellType = auction.SellType;
                updateAuction.AuctionDetail.ExpDate = auction.ExpDate;
                //save updated record
                if (auction.BuyerId != "" && auction.BuyerId != null)
                    await _userRepo.UpdateUserById(auction.BuyerId, auction.Buyer);
                await _userRepo.UpdateUserById(auction.SellerId, auction.Seller);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Updating Auction: " + e);
            }
            

            return false;
        }

        //requires auction Ids to be int convertable
        public string IdGen()
        {
            var lastAuctionId = _context.Auctions.Select(x=>Convert.ToInt32(x.AuctionId)).ToList().Max();
            string newId = Convert.ToString(lastAuctionId + 1);
            return newId;
        }
    }
}
