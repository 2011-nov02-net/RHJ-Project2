using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DataAccess.Entities.Repo.Interfaces;

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
            if (dbUsers == null) return null;
            var appUsers = DomainDataMapper.GetAllUsers(dbUsers);
            return appUsers;

        }

        public async Task<AppUser> GetOneUser(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbUser = await context.Customers.FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser == null) return null;
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            return appUser;
        }

        // need to handle duplicate outside
        public async Task<AppUser> AddOneUser(AppUser user)
        {           
            using var context = new Project2Context(_contextOptions);
            var newUser = DomainDataMapper.AddOneUser(user);
            await context.Customers.AddAsync(newUser);
            await context.SaveChangesAsync();
            return user;
        }    

        
        // not mapped
        public async Task<IEnumerable<AppCard>> GetAllCardsOfOneUser(string id)
        {
            using var context = new Project2Context(_contextOptions);            
            var dbInv= await context.UserCardInventories
                                    .Include(x => x.Card).Where(x => x.UserId == id).ToListAsync();
            if (dbInv == null) return null;
            var appCards = dbInv.Select(x => new AppCard
            {
                CardId = x.CardId,
                Name = x.Card.Name,
                Type = x.Card.Type,
                Rarity = x.Card.Rarity,
                Value = x.Card.Value,
            });
            return appCards;
        }
        
        // not mapped
        public async Task<AppCard> GetOneCardOfOneUser(string id, string cardId)
        {
            using var context = new Project2Context(_contextOptions);           
            var dbInv = await context.UserCardInventories.FirstOrDefaultAsync(x => x.UserId == id && x.CardId == cardId);
            if (dbInv == null) return null;
            var appCard = new AppCard
            {
                CardId = dbInv.CardId,
                Name = dbInv.Card.Name,
                Type = dbInv.Card.Type,
                Rarity = dbInv.Card.Rarity,
                Value = dbInv.Card.Value,
            };
            return appCard;
        }

        public async Task AddOneCardToOneUser(string id, AppCard card)
        {           
            using var context = new Project2Context(_contextOptions);                       
            var dbInv = await context.UserCardInventories.FirstOrDefaultAsync(x => x.UserId == id && x.CardId == card.CardId);
            if (dbInv != null)
            {
                // if already has the card
                dbInv.Quantity += 1;
                await context.SaveChangesAsync();              
            }
            else
            {
                // if not 
                var newRecord = new UserCardInventory
                {
                    UserId = id,
                    CardId = card.CardId,
                    Quantity = 1,
                };
                await context.UserCardInventories.AddAsync(newRecord);
                await context.SaveChangesAsync();
            }                      
        }
        
        public async Task<string> DeleteOneCardOfOneUser(string id, string cardId)
        { 
            using var context = new Project2Context(_contextOptions);
            var dbUser = await context.Customers
                                    .Include(x => x.UserCardInventories)
                                    .ThenInclude(x => x.Card).FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser == null) return null;
            var dbInv = dbUser.UserCardInventories.FirstOrDefault(x => x.CardId == cardId);
            if (dbInv != null)
            {
                context.UserCardInventories.Remove(dbInv);
                await context.SaveChangesAsync();
            }
            else
            {
                // if card does not exist
                return null;
            }
            return "Deleted";
        }
    }
}
