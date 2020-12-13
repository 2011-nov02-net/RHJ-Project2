using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class UserRepo
    {

        private readonly DbContextOptions<Project2Context> _contextOptions;
        public UserRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            using var context = new Project2Context(_contextOptions);
            var dbUsers = await context.Customers.ToListAsync();
            var appUsers = DomainDataMapper.GetAllUsers(dbUsers);
            return appUsers;

        }

        public async Task<AppUser> GetOneUser(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbUser = await context.Customers.FirstOrDefaultAsync(x => x.UserId == id);
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            return appUser;
        }

        public async Task AddOneUser(AppUser user)
        { 
            // check duplicates outside
            using var context = new Project2Context(_contextOptions);
            var newUser = DomainDataMapper.AddOneUser(user);
            await context.Customers.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        public IEnumerable<AppCard> GetAllCardsOfOneUser(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbCards = context.Customers
                                    .Include(x => x.UserCardInventories)
                                    .ThenInclude(x=> x.Cards).FirstOrDefault(x => x.UserId == id);
            if (dbCards == null) return null;

            //
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


        public void AddOneCardToOneUser(string id, AppCard card)
        { 
            using var context = new Project2Context(_contextOptions);
            var dbInventories = context.Customers
                                        .Include(x => x.UserCardInventories)
                                        .FirstOrDefault(x => x.UserId == id);
            // check duplicates outside
            // consolidate numbers
            var newBridge = new UserCardInventory
            {
                UserId = id,
                CardId = card.CardId,
                Quantity = 1,             
            };
            context.UserCardInventories.Add(newBridge);
            context.SaveChanges();

           



             
            



        }









    }
}
