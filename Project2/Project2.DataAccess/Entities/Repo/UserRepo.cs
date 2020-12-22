using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DataAccess.Entities.Repo.Interfaces;
using System.Diagnostics;

namespace Project2.DataAccess.Entities.Repo
{
    public class UserRepo: IUserRepo
    {

        private readonly Project2Context _context;
        public UserRepo(Project2Context contextOptions)
        {
            _context = contextOptions;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {         
            var dbUsers = await _context.Customers.ToListAsync();
            if (dbUsers == null) return null;
            var appUsers = DomainDataMapper.GetAllUsers(dbUsers);
            return appUsers;
        }

        public async Task<AppUser> GetOneUserByEmail(string email)
        {
            var dbUser = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
            if (dbUser == null) return null;
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            return appUser;
        }

        public async Task<AppUser> GetOneUser(string id)
        {
             
            var dbUser = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser == null) return null;
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            return appUser;
        }
      
        public async Task AddOneUser(AppUser user)
        {           
             
            var newUser = DomainDataMapper.AddOneUser(user);
            await _context.Customers.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }    
       
        // not mapped
        public async Task<IEnumerable<AppCard>> GetAllCardsOfOneUser(string id)
        {
                       
            var dbInv= await _context.UserCardInventories
                                    .Include(x => x.Card).Where(x => x.UserId == id).ToListAsync();
            if (dbInv == null) return null;
            var appCards = dbInv.Select(x => new AppCard(x.Card.Rating, x.Card.NumOfRatings, x.Card.Rarity)
            {
                CardId = x.CardId,
                Name = x.Card.Name,
                Type = x.Card.Type,
                Image = x.Card.Image
            });
            return appCards;
        }
        
        // not mapped
        public async Task<AppCard> GetOneCardOfOneUser(string id, string cardId)
        {
                       
            var dbInv = await _context.UserCardInventories.Include(x => x.Card).FirstOrDefaultAsync(x => x.UserId == id && x.CardId == cardId);
            if (dbInv == null) return null;
            var appCard = new AppCard(dbInv.Card.Rating, dbInv.Card.NumOfRatings,dbInv.Card.Rarity)
            {
                CardId = dbInv.CardId,
                Name = dbInv.Card.Name,
                Type = dbInv.Card.Type,
                Image = dbInv.Card.Image
            };
            return appCard;
        }

        public async Task AddOneCardToOneUser(string id, AppCard card)
        {           
                                   
            var dbInv = await _context.UserCardInventories.FirstOrDefaultAsync(x => x.UserId == id && x.CardId == card.CardId);
            if (dbInv != null)
            {
                // if already has the card
                dbInv.Quantity += 1;
                await _context.SaveChangesAsync();              
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
                await _context.UserCardInventories.AddAsync(newRecord);
                await _context.SaveChangesAsync();
            }                      
        }
        
        public async Task DeleteOneCardOfOneUser(string id, string cardId)
        { 
                      
            var dbInv = await _context.UserCardInventories.FirstOrDefaultAsync(x => x.UserId == id && x.CardId == cardId);           
            if (dbInv != null)
            {
                _context.UserCardInventories.Remove(dbInv);
                await _context.SaveChangesAsync();
            }
            else
            {
                // if card does not exist
                throw new Exception("You do not even have this card in your inventory");
            }
             
        }

        public async Task<bool> UpdateUserById(string id, AppUser updateUser)
        {
            var dbUser = await _context.Customers.FirstOrDefaultAsync(x=>x.UserId == id);
            if (dbUser == null || updateUser == null)
                return false;
            else
            {
                try
                {
                    //only necessary to modify currencyAmount and packs purchased in the db
                    dbUser.CurrencyAmount = updateUser.CurrencyAmount;
                    dbUser.NumPacksPurchased = updateUser.NumPacksPurchased;

                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Updating User: " + e);
                }
            }
            return false;
        }

    }
}
