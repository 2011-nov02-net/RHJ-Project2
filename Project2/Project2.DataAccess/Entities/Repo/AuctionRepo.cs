﻿using Microsoft.EntityFrameworkCore;
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
        public AuctionRepo(Project2Context context )
        {
            _context  = context ;
        }

        //Get all auctions
        public async Task<IEnumerable<AppAuction>> GetAllAuctions()
        {       
            var dbAuctions = await _context.Auctions.ToListAsync();

            if (dbAuctions == null)
                return null;

            var appAuctions = DomainDataMapper.GetAllAuctions(dbAuctions);

            return appAuctions;
        }

        //Get auction by id
        public async Task<AppAuction> GetAuctionById(string id)
        {
     
            var dbAuctions = await _context.Auctions.FirstOrDefaultAsync(x => x.AuctionId == id);
            if (dbAuctions == null) 
                return null;

            var appAuction = DomainDataMapper.GetAuction(dbAuctions);
            return appAuction;
        }

        //get auction details by id
        public async Task<AppAuction> GetAuctionDetailById(string id)
        {
 
            var dbDetail = await _context.AuctionDetails.FirstOrDefaultAsync(x => x.AuctionId == id);
            if (dbDetail == null)
                return null;

            var appAuctionDetail = DomainDataMapper.GetAuctionDetail(dbDetail);
            return appAuctionDetail;
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

        //Update auction by id
        public async Task<bool> UpdateAuction(string id, AppAuction auction)
        {
 
            var updateAuction = await _context.Auctions.Where(x => x.AuctionId == id).FirstAsync();
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
                    await _context.SaveChangesAsync();

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
 
            var updateAuctionDetail = await _context.AuctionDetails.Where(x => x.AuctionId == id).FirstAsync();
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
                    await _context.SaveChangesAsync();

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
