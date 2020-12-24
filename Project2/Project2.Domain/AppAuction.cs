using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppAuction
    {
        public string AuctionId { get; set; }
        public AppUser Seller { get; set; }
        public string SellerId { get; set; }
        public AppUser Buyer { get; set; }
        public string BuyerId { get; set; }
        public AppCard Card { get; set; }
        public string CardId { get; set; }
        public double PriceListed { get; set; }
        public double BuyoutPrice { get; set; }
        public double? PriceSold { get; set; }
        public int? NumberBids { get; set; }
        public string SellType { get; set; }
        public DateTime ExpDate { get; set; } 
        public DateTime? SellDate { get; set; }
        public AppAuction(string Id, AppUser seller, AppCard card)
        {
            this.AuctionId = Id;
            this.Seller = seller;
            this.Card = card;
            this.ExpDate = DateTime.UtcNow.AddDays(3);
            //initialize minimum bid
            this.PriceListed = card.Value / 4;
            this.BuyoutPrice = card.Value * 2;
            this.NumberBids = 0;

        }
        public AppAuction()
        {

        }

        /// <summary>
        /// updates the auction with a new bid
        /// </summary>
        /// <param name="bidTime"></param>
        /// <param name="bidAmount"></param>
        /// <param name="bidder"></param>
        public void NewBid(DateTime bidTime, double bidAmount)
        {
            //update bid if auction has not expired and bid is at least 50 cents higher than current price
            if(bidAmount > .50 && DateTime.Compare(ExpDate, bidTime) > 0)
            {

                //PriceListed += bidAmount; //this is handled on the front end
                NumberBids = ++NumberBids ?? 1;
            }
            else if (bidAmount <= .50)
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
        /// <summary>
        /// buyer bought out the auction, update buyer and close auction
        /// </summary>
        /// <param name="buyer"></param>
        public void BuyOut(/*AppUser buyer*/) //true if successful
        {
            if (Buyer.CurrencyAmount > this.BuyoutPrice)
            {
                //Buyer = buyer;
                Buyer.AddCardToInventory(Card);
                Seller.RemoveCardFromInventory(Card);
                Buyer.CurrencyAmount -= this.BuyoutPrice;
                Seller.CurrencyAmount += BuyoutPrice;
                this.PriceSold = this.BuyoutPrice;
                this.SellDate = DateTime.UtcNow;
                this.SellType = "Buyout"; //this seems odd
            }
            else
            {
                throw new ArgumentException("buyer does not have sufficient funds.");
            }
        }
        /// <summary>
        /// Updates the AppAuction if it has expired
        /// </summary>
        /// <returns>Called AppAuction object</returns>
        public bool Expired()
        {
            if (NumberBids != null && DateTime.Compare(ExpDate, DateTime.UtcNow) <= 0)
            {
                //Buyer.AddCardToInventory(Card);
                //Seller.RemoveCardFromInventory(Card);
                Buyer.CurrencyAmount -= this.PriceListed;
                Seller.CurrencyAmount += PriceListed;
                this.PriceSold = this.PriceListed;
                this.SellDate = ExpDate;
                this.SellType = "Bid"; //this seems odd
                return true;
            }
            else if ((NumberBids == null || NumberBids == 0) && DateTime.Compare(ExpDate, DateTime.UtcNow) <= 0)
            {
                this.PriceSold = 0;
                this.SellDate = ExpDate;
                this.SellType = "None"; //expired with no bids
                return true;
            }
            return false;
        }
    }
}
