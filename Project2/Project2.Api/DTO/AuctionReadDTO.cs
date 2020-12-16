using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class AuctionReadDTO
    {
        public string AuctionId { get; set; }
        public string SellerId { get; set; }
        public string  BuyerId { get; set; }
        public string CardId { get; set; }
        [Range(0,1000)]
        public double PriceSold { get; set; }
        public DateTime SellDate { get; set; }
    }
}
