using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppUser
    {
        public string UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
        public string UserRole { get; set; }
        public int NumPacksPurchased { get; set; } = 0;
        public double CurrencyAmount { get; set; }
        public List<AppCard> Inventory { get; set; } = new List<AppCard>();
        public List<AppAuction> AuctionHistory { get; set; } = new List<AppAuction>();
        public List<AppOrder> OrderHistory { get; set; } = new List<AppOrder>();
        public List<AppTrade> TradeHistory { get; set; } = new List<AppTrade>();

        public AppUser(string userId, string firstName, string lastName, string email, string UserRole)
        {
            this.UserId = userId;
            this.First = firstName;
            this.Last = lastName;
            this.Email = email;
            this.UserRole = UserRole;
        }

        public AppUser()
        {

        }
    }
}
