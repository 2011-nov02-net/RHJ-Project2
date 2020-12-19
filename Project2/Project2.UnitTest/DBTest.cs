using Project2.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.UnitTest
{
    public class DBTest
    {
        public static List<Customer> DBUsers()
        {
            var sessions = new List<Customer>();
            sessions.Add(new Customer
            {
                UserId = "cus1",
                First = "Kyle",
                Last = "Crane",
                Email = "KC@gmail.com",
                UserRole = "User",
                NumPacksPurchased = 1,
                CurrencyAmount = 10,
            });
            sessions.Add(new Customer
            {
                UserId = "cus2",
                First = "NaN",
                Last = "Man",
                Email = "NN@gmail.com",
                UserRole = "User",
                NumPacksPurchased = 2,
                CurrencyAmount = 20,
            });
            return sessions;
        }

        public static List<Card> DBCards()
        {
            var sessions = new List<Card>();
            sessions.Add(new Card
            {
                CardId = "card101",
                Name = "Mew",
                Type = "Psychic",
                Rarity = 5,
                Value = 10,
            });
            sessions.Add(new Card
            {
                CardId = "card102",
                Name = "MewTwo",
                Type = "Psychic",
                Rarity = 6,
                Value = 15,
            });
            sessions.Add(new Card
            {
                CardId = "card103",
                Name = "Pinsir",
                Type = "Bug",
                Rarity = 4,
                Value = 5,
            });
            return sessions;
        }

        public static List<StoreInventory> DBItems()
        {
            var sessions = new List<StoreInventory>();
            sessions.Add(new StoreInventory
            {
                PackId = "pack101",
                PackQty = 10,
            });
            sessions.Add(new StoreInventory
            {
                PackId = "pack101",
                PackQty = 10,
            });
            return sessions;
        }

    }
}
