using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            AuctionBuyers = new HashSet<Auction>();
            AuctionSellers = new HashSet<Auction>();
            Orders = new HashSet<Order>();
            TradeBuyers = new HashSet<Trade>();
            TradeOfferers = new HashSet<Trade>();
            UserCardInventories = new HashSet<UserCardInventory>();
        }

        public string UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public int NumPacksPurchased { get; set; }
        public double CurrencyAmount { get; set; }

        public virtual ICollection<Auction> AuctionBuyers { get; set; }
        public virtual ICollection<Auction> AuctionSellers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Trade> TradeBuyers { get; set; }
        public virtual ICollection<Trade> TradeOfferers { get; set; }
        public virtual ICollection<UserCardInventory> UserCardInventories { get; set; }
    }
}
