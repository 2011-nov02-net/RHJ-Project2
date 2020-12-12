using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api.Entities
{
    public partial class Auction
    {
        public string AuctionId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public string CardId { get; set; }
        public double PriceSold { get; set; }
        public DateTime SellDate { get; set; }

        public virtual Customer Buyer { get; set; }
        public virtual Card Card { get; set; }
        public virtual Customer Seller { get; set; }
        public virtual AuctionDetail AuctionDetail { get; set; }
    }
}
