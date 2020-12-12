using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppTrade
    {
        public string Id { get; }
        public AppUser Offerer { get; }
        public AppUser Buyer { get; set; }
        public DateTime TradeDate { get; set; }
        public AppCard OfferCard { get; set; }
        public AppCard BuyerCard { get; set; }
        AppTrade(string id, AppUser offerer, AppUser buyer, AppCard offerCard, AppCard buyCard)
        {
            this.Id = id;
            this.Offerer = offerer;
            this.Buyer = buyer;
            this.OfferCard = offerCard;
            this.BuyerCard = buyCard;
        }
    }
}
