using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class AuctionDetail
    {
        public string AuctionId { get; set; }
        public double PriceListed { get; set; }
        public double BuyoutPrice { get; set; }
        public int? NumberBids { get; set; }
        public string SellType { get; set; }
        public DateTime ExpDate { get; set; }

        public virtual Auction Auction { get; set; }
    }
}
