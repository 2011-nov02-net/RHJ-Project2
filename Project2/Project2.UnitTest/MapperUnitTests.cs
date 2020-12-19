using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Project2.UnitTest
{
    public class MapperUnitTests
    {
        [Fact]
        public void DomainDateMapper_User_GetOneUser()
        {
            var dbUser = DBTest.DBUsers()[0];
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            Assert.Equal(dbUser.UserId, appUser.UserId);
            Assert.Equal(dbUser.First, appUser.First);
            Assert.Equal(dbUser.Last, appUser.Last);
            Assert.Equal(dbUser.Email, appUser.Email);
            Assert.Equal(dbUser.UserRole, appUser.UserRole);
            Assert.Equal(dbUser.NumPacksPurchased, appUser.NumPacksPurchased);
            Assert.Equal(dbUser.CurrencyAmount, appUser.CurrencyAmount);
        }

        [Fact]            
        public void DomainDateMapper_User_AddOneUser()
        {
            var appUser = Test.Users()[0];
            var dbUser = DomainDataMapper.AddOneUser(appUser);
            Assert.Equal(expected: appUser.UserId, actual: dbUser.UserId);
            Assert.Equal(appUser.First, dbUser.First);
            Assert.Equal(appUser.Last, dbUser.Last);
            Assert.Equal(appUser.Email, dbUser.Email);
            Assert.Equal(appUser.UserRole, dbUser.UserRole);
            Assert.Equal(appUser.NumPacksPurchased, dbUser.NumPacksPurchased);
            Assert.Equal(appUser.CurrencyAmount, dbUser.CurrencyAmount);
        }

        [Fact]
        public void DomainDateMapper_Card_GetOneCard()
        {
            var dbCard = DBTest.DBCards()[0];
            var appCard = DomainDataMapper.GetOneCard(dbCard);
            Assert.Equal(dbCard.CardId, appCard.CardId);
            Assert.Equal(dbCard.Name, appCard.Name);
            Assert.Equal(dbCard.Type, appCard.Type);
        }

        [Fact]
        public void DomainDateMapper_Card_AddOneCard()
        {
            var appCard = Test.Cards()[0];
            var dbCard = DomainDataMapper.AddOneCard(appCard);
            Assert.Equal(appCard.CardId, dbCard.CardId);
            Assert.Equal(appCard.Name, dbCard.Name);
            Assert.Equal(appCard.Type, dbCard.Type);
        }

        [Fact]
        public void DomainDateMapper_Store_GetOneItem()
        {
            var dbItem = DBTest.DBItems()[0];
            var appItem = DomainDataMapper.GetStorePackById(dbItem);
            Assert.Equal(dbItem.PackId, appItem.PackId);
            Assert.Equal(dbItem.PackQty, appItem.PackQty);
        }
    }
}
