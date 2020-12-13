using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppAuction
    {
        public string AuctionId { get; }
        public AppUser Seller { get; }
        public AppUser Buyer { get;  }
        public AppCard Card { get;  }
        public double PriceListed { get; set; }
        public double BuyoutPrice { get; set; }
        public double PriceSold { get; set; }
        public int? NumberBids { get; set; }
        public string SellType { get; set; }
        public DateTime ExpDate { get; set; } 
        public DateTime SellDate { get; set; }
        public AppAuction(string Id, AppUser seller, AppUser buyer, AppCard card)
        {
            this.AuctionId = Id;
            this.Seller = seller;
            this.Buyer = buyer;
            this.Card = card;
            this.ExpDate = DateTime.Now.AddDays(3);
            //initialize minimum bid
            this.PriceListed = card.Value / 4;
            this.BuyoutPrice = card.Value * 2;
            this.NumberBids = 0;

        }

        // NewBid(), Sold()
    }
}
