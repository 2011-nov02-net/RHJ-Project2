using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppAuction
    {
        public string AuctionId { get; set; }
        public AppUser Seller { get; }
        public AppUser Buyer { get; set; }
        public AppCard Card { get;  }
        public double PriceListed { get; set; }
        public double BuyoutPrice { get; set; }
        public double PriceSold { get; set; }
        public int? NumberBids { get; set; }
        public string SellType { get; set; }
        public DateTime ExpDate { get; set; } 
        public DateTime SellDate { get; set; }
        public AppAuction(string Id, AppUser seller, AppCard card)
        {
            this.AuctionId = Id;
            this.Seller = seller;
            this.Card = card;
            this.ExpDate = DateTime.Now.AddDays(3);
            //initialize minimum bid
            this.PriceListed = card.Value / 4;
            this.BuyoutPrice = card.Value * 2;
            this.NumberBids = 0;

        }
        public AppAuction(string Id, string sellerId, string cardId)
        {
            this.AuctionId = Id;
            this.Seller.UserId = sellerId;
            this.Card.CardId = cardId;
            this.ExpDate = DateTime.Now.AddDays(3);
            //initialize minimum bid
            this.PriceListed = this.Card.Value / 4;
            this.BuyoutPrice = this.Card.Value * 2;
            this.NumberBids = 0;

        }

        // NewBid(), Sold(), Buyout()
        /// <summary>
        /// updates the auction with a new bid
        /// </summary>
        /// <param name="bidTime"></param>
        /// <param name="bidAmount"></param>
        /// <param name="bidder"></param>
        public void NewBid(DateTime bidTime, double bidAmount, AppUser bidder)
        {
            double bidDifference = bidAmount - PriceListed;
            //update bid if auction has not expired and bid is at least 50 cents higher than current price
            if(bidDifference > .50 && DateTime.Compare(ExpDate, bidTime) > 0)
            {
                PriceListed = bidAmount;
                Buyer = bidder;
            }
            else if (bidDifference <= .50)
            {
                throw new ArgumentException("invalid bid value", "bidAmount");
            }
            else if (DateTime.Compare(ExpDate, bidTime) <= 0)
            {
                throw new ArgumentException("bid recieved after auction expired", "bidTime");
            }
            else
            {
                throw new Exception("some unexpected exception while creating bid.");
            }
        }
    }
}
