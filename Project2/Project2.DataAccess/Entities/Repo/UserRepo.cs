using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class UserRepo: IUserRepo
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

        public async Task AddOneCardToOneUser(string id, AppCard card)
        { 
            using var context = new Project2Context(_contextOptions);
            var dbInventories = await context.Customers
                                        .Include(x => x.UserCardInventory)
                                        .FirstOrDefaultAsync(x => x.UserId == id);
            // if already has the card
            foreach (var record in dbInventories.UserCardInventory)
            {
                if (record.CardId == card.CardId)
                {
                    record.Quantity = record.Quantity + 1;
                    await context.SaveChangesAsync();
                    return;
                }
            }
            // if not 
            var newBridge = new UserCardInventory
            {
                UserId = id,
                CardId = card.CardId,
                Quantity = 1,             
            };
            await context.UserCardInventories.AddAsync(newBridge);
            await context.SaveChangesAsync();
        }

        public Task<IEnumerable<AppCard>> GetAllCardsOfOneUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AppCard> GetOneCardOfOneUser(string id, string cardId)
        {
            throw new NotImplementedException();
        }
    }
}
