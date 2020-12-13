using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppUser
    {
        public string UserId { get; }
        public string First { get; }
        public string Last { get; }
        public string Email { get;}
        public bool IsManager { get; }
        public string UserRole { get; }
        public int NumPacksPurchased { get; set; } = 0;
        public double CurrencyAmount { get; set; }
        public List<AppCard> Inventory { get; set; } = new List<AppCard>();
        public List<AppAuction> AuctionHistory { get; set; } = new List<AppAuction>();
        public List<AppOrder> OrderHistory { get; set; } = new List<AppOrder>();
        public List<AppTrade> TradeHistory { get; set; } = new List<AppTrade>();

        AppUser(string userId, string firstName, string lastName, string email, string UserRole)
        {
            this.UserId = userId;
            this.First = firstName;
            this.Last = lastName;
            this.Email = email;
            this.UserRole = UserRole;
        }
    }
}
