using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class TradeDetail
    {
        public string TradeId { get; set; }
        public string OfferCardId { get; set; }
        public string BuyerCardId { get; set; }

        public virtual Card BuyerCard { get; set; }
        public virtual Card OfferCard { get; set; }
        public virtual Trade Trade { get; set; }
    }
}
