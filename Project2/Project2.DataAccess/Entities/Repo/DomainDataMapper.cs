using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.DataAccess.Entities.Repo
{
    // will start mapping later
    public static class DomainDataMapper
    {
        // cards
        public static IEnumerable<AppCard> GetAllCards(IEnumerable<Card> dbCards)
        {
            var appCards = dbCards.Select(x => new AppCard
            {
                CardId = x.CardId,
                Name = x.Name,
                Type = x.Type,
                Rarity = x.Rarity,
                Value = x.Value,
            });
            return appCards;

        }
        public static AppCard GetOneCard(Card dbCard)
        {
            var appCard = new AppCard
            {
                CardId = dbCard.CardId,
                Name = dbCard.Name,
                Type = dbCard.Type,
                Rarity = dbCard.Rarity,
                Value = dbCard.Value,
            };
            return appCard;
        }
        public static Card AddOneCard(AppCard card)
        {
            var newCard = new Card
            {
                CardId = card.CardId,
                Name = card.Name,
                Type = card.Type,
                Rarity = card.Rarity,
                Value = card.Value,
            };
            return newCard;
        }

        // users
        public static IEnumerable<AppUser> GetAllUsers(IEnumerable<Customer> dbUsers)
        {
            var appUsers = dbUsers.Select(x => new AppUser
            {
                UserId = x.UserId,
                First = x.First,
                Last = x.Last,
                Email = x.Email,
                UserRole = x.UserRole,
                NumPacksPurchased = x.NumPacksPurchased,
                CurrencyAmount = x.CurrencyAmount,
            });
            
            return appUsers;
        }

        public static AppUser GetOneUser(Customer dbUser)
        {
            var appUser = new AppUser
            {
                UserId = dbUser.UserId,
                First = dbUser.First,
                Last = dbUser.Last,
                Email = dbUser.Email,
                UserRole = dbUser.UserRole,
                NumPacksPurchased = dbUser.NumPacksPurchased,
                CurrencyAmount = dbUser.CurrencyAmount,

            };
            return appUser;
        }

        public static Customer AddOneUser(AppUser user)
        {
            var newUser = new Customer
            {
                UserId = user.UserId,
                First = user.First,
                Last = user.Last,
                Email = user.Email,
                UserRole = user.UserRole,
                NumPacksPurchased = user.NumPacksPurchased,
                CurrencyAmount = user.CurrencyAmount,
            };
            return newUser;
        }

        //store packs
        public static IEnumerable<AppStoreItem> GetAllStorePacks(IEnumerable<StoreInventory> dbPacks)
        {
            var appPacks = dbPacks.Select(x => new AppStoreItem(x.PackId, x.PackQty));
            return appPacks;
        }

        public static AppStoreItem GetStorePackById(StoreInventory dbPack)
        {
            var storePack = new AppStoreItem(dbPack.PackId, dbPack.PackQty);
            return storePack;
        }

        //auctions
        /*public static IEnumerable<AppAuction> GetAllAuctions(IEnumerable<Auction> dbAuctions)
        {
            var appAuctions = dbAuctions.Select(x => new AppAuction(x.AuctionId, x.Seller, x.Card));
            return appAuctions;
        }*/
    }
}
