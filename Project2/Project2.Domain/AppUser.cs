using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppUser
    {
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get;}
        public bool IsManager { get; }
        public double CurrencyAmount { get; set; }
        public List<AppCard> Inventory { get; set; } = new List<AppCard>();
        public List<AppAuction> AuctionHistory { get; set; } = new List<AppAuction>();
        public List<AppOrder> OrderHistory { get; set; } = new List<AppOrder>();
        public List<AppTrade> TradeHistory { get; set; } = new List<AppTrade>();

        AppUser(string id, string firstName, string lastName, string email, bool isManager)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.IsManager = isManager;
        }
    }
}
