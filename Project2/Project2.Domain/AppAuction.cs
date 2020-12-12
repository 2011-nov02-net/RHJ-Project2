﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppAuction
    {
        public string Id { get; }
        public AppUser Seller { get; }
        public AppUser Buyer { get;  }
        public AppCard Card { get;  }
        public double PriceListed { get; }
        public double BuyoutPrice { get; set; }
        public double PriceSold { get; }
        public int? NumberBids { get; }
        public string SellType { get; }
        public DateTime ExpDate { get; set; } 
        public DateTime SellDate { get; }
        AppAuction(string Id, AppUser seller, AppUser buyer, AppCard card)
        {
            this.Id = Id;
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