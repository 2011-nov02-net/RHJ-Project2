using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppTrade
    {
        public string TradeId { get; set; }
        public AppUser Offerer { get; set; }
        public string OffererId { get; set; }
        public AppUser Buyer { get; set; }
        public string BuyerId { get; set; }
        public DateTime TradeDate { get; set; }
        public AppCard OfferCard { get; set; }
        public string OfferCardId { get; set; }
        public AppCard BuyerCard { get; set; }
        public string BuyerCardId { get; set; }
        public bool IsClosed { get; set; }
        public AppTrade(string id, AppUser offerer, AppUser buyer, AppCard offerCard, AppCard buyCard)
        {
            this.TradeId = id;
            this.Offerer = offerer;
            this.Buyer = buyer;
            this.OfferCard = offerCard;
            this.BuyerCard = buyCard;
        }
        public AppTrade()
        {
        }
    }
}
