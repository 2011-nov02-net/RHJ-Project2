using Project2.Api.DTO;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.UnitTest
{
    public static class Test
    {
        // regular versions are for _mockRepo.Setup(x)
        // DTO versions are for controller.action(x)
       
        public static List<AppUser> Users()
        {
            var sessions = new List<AppUser>();
            sessions.Add(new AppUser
            {
                UserId = "cus1",
                First = "Kyle",
                Last = "Crane",
                Email = "KC@gmail.com",
                UserRole = "User",
                NumPacksPurchased = 1,
                CurrencyAmount = 10,
            });
            sessions.Add(new AppUser
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

        public static List<AppCard> Cards()
        {
            var sessions = new List<AppCard>();
            sessions.Add(new AppCard
            {
                CardId = "card101",
                Name = "Mew",
                Type = "Psychic",
                Rarity = 5,
                Value = 10,
            });
            sessions.Add(new AppCard
            {
                CardId = "card102",
                Name = "MewTwo",
                Type = "Psychic",
                Rarity = 6,
                Value = 15,
            });
            sessions.Add(new AppCard
            {
                CardId = "card103",
                Name = "Pinsir",
                Type = "Bug",
                Rarity = 4,
                Value = 5,
            });
            return sessions;
        }

        public static List<AppStoreItem> Items()
        {
            var sessions = new List<AppStoreItem>();
            sessions.Add(new AppStoreItem
            {
                PackId = "pack101",
                PackQty = 10,
            });
            sessions.Add(new AppStoreItem
            {
                PackId = "pack101",
                PackQty = 10,
            });
            return sessions;
        }


        // UserReadDTO does not have userRole
        public static List<UserCreateDTO> UsersDTO()
        {
            var sessions = new List<UserCreateDTO>();
            sessions.Add(new UserCreateDTO
            {
                UserId = "cus1",
                First = "Kyle",
                Last = "Crane",
                Email = "KC@gmail.com",
                NumPacksPurchased = 1,
                CurrencyAmount = 10,
            });
            sessions.Add(new UserCreateDTO
            {
                UserId = "cus2",
                First = "NaN",
                Last = "Man",
                Email = "NN@gmail.com",
                NumPacksPurchased = 2,
                CurrencyAmount = 20,
            });
            return sessions;
        }

        public static List<CardCreateDTO> CardsDTO()
        {
            var sessions = new List<CardCreateDTO>();
            sessions.Add(new CardCreateDTO
            {
                CardId = "card101",
                Name = "Mew",
                Type = "Psychic",
                Rarity = 5,
                Value = 10,
            });
            sessions.Add(new CardCreateDTO
            {
                CardId = "card102",
                Name = "MewTwo",
                Type = "Psychic",
                Rarity = 6,
                Value = 15,
            });
            sessions.Add(new CardCreateDTO
            {
                CardId = "card103",
                Name = "Pinsir",
                Type = "Bug",
                Rarity = 4,
                Value = 5,
            });
            return sessions;
        }



    }
}
