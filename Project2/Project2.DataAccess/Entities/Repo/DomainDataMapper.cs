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
            
            var appCards = new List<AppCard>();
            foreach (var dbCard in dbCards)
            {
                var appCard = new AppCard(dbCard.Rating, dbCard.NumOfRatings,dbCard.Rarity)
                {
                    CardId = dbCard.CardId,
                    Name = dbCard.Name,
                    Type = dbCard.Type,
                    Image = dbCard.Image,
                };
                appCards.Add(appCard);
            }
            return appCards;

        }
        public static AppCard GetOneCard(Card dbCard)
        {
            var appCard = new AppCard(dbCard.Rating, dbCard.NumOfRatings, dbCard.Rarity)
            {
                CardId = dbCard.CardId,
                Name = dbCard.Name,
                Type = dbCard.Type,
                Image = dbCard.Image,
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
                Image = card.Image,
                Rating = card.Rating,
                NumOfRatings = card.NumOfRatings,
                Value = card.Value
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
/*        public static IEnumerable<AppAuction> GetAllAuctions(IEnumerable<Auction> dbAuctions, IEnumerable<AuctionDetail> dbAuctionDetails)
        {
            var appAuctions = new List<AppAuction>();

            foreach (var dbAuction in dbAuctions)
            {
                var appAuction = new AppAuction() 
                {
                    AuctionId = dbAuction.AuctionId,
                    SellerId = dbAuction.SellerId,
                    BuyerId = dbAuction.BuyerId,
                    CardId = dbAuction.CardId,
                    PriceSold = (double)dbAuction.PriceSold,
                    SellDate = (DateTime)dbAuction.SellDate,
                    PriceListed = dbAuctionDetail.PriceListed,
                    BuyoutPrice = dbAuctionDetail.BuyoutPrice,
                    NumberBids = dbAuctionDetail.NumberBids,
                    SellType = dbAuctionDetail.SellType,
                    ExpDate = dbAuctionDetail.ExpDate
                };
                appAuctions.Add(appAuction);
            }

            return appAuctions;
        }*/

        public static AppAuction GetAuction(Auction dbAuction)
        {
            var appAuction = new AppAuction()
            {
                AuctionId = dbAuction.AuctionId,
                SellerId = dbAuction.SellerId,
                BuyerId = dbAuction.BuyerId,
                CardId = dbAuction.CardId,
                PriceSold = (double)dbAuction.PriceSold,
                SellDate = (DateTime)dbAuction.SellDate
            };

            return appAuction;
        }

        public static AppAuction GetAuctionDetail(AuctionDetail dbAuction)
        {
            var appAuctionDetail = new AppAuction()
            {
                AuctionId = dbAuction.AuctionId,
                PriceListed = dbAuction.PriceListed,
                BuyoutPrice = dbAuction.BuyoutPrice,
                NumberBids = dbAuction.NumberBids,
                SellType = dbAuction.SellType,
                ExpDate = dbAuction.ExpDate
            };

            return appAuctionDetail;
        }
    }
}
