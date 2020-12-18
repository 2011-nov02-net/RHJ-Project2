using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class TradeReadDTO
    {
        public string TradeId { get; set; }
        public string OffererId { get; set; }
        public string BuyerId { get; set; }
        public bool IsClosed { get; set; }
        public DateTime TradeDate { get; set; }
        //TradeDetails
        public string OfferCardId { get; set; }
        public string BuyerCardId { get; set; }
    }
}
