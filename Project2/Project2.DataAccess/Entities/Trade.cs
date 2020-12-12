using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class Trade
    {
        public string TradeId { get; set; }
        public string OffererId { get; set; }
        public string BuyerId { get; set; }
        public DateTime TradeDate { get; set; }

        public virtual Customer Buyer { get; set; }
        public virtual Customer Offerer { get; set; }
        public virtual TradeDetail TradeDetail { get; set; }
    }
}
