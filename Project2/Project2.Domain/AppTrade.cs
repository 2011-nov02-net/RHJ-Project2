﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppTrade
    {
        public string TradeId { get; }
        public AppUser Offerer { get; }
        public AppUser Buyer { get; }
        public DateTime TradeDate { get; set; }
        public AppCard OfferCard { get; }
        public AppCard BuyerCard { get; }
        public AppTrade(string id, AppUser offerer, AppUser buyer, AppCard offerCard, AppCard buyCard)
        {
            this.TradeId = id;
            this.Offerer = offerer;
            this.Buyer = buyer;
            this.OfferCard = offerCard;
            this.BuyerCard = buyCard;
        }
    }
}
