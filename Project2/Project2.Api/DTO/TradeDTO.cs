using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class TradeDTO
    {
        public string TradeId { get; set; }
        public string OffererId { get; set; }

        // isClosed is not needed

        public string BuyerId { get; set; }        
        public DateTime TradeDate { get; set; }
        // evolve
    }
}
