using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class CardRepo
    {
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public CardRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<AppCard>> GetAllCards()
        {
            using var context = new Project2Context(_contextOptions);
            var dbCards = await context.Cards.ToListAsync();
            if (dbCards == null) return null;           
            var appCards = DomainDataMapper.GetAllCards(dbCards); 
            return appCards;
        }

        public async Task<AppCard> GetOneCard(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbCard = await context.Cards.FirstOrDefaultAsync(x => x.CardId == id);
            if (dbCard == null) return null;
            var appCard = DomainDataMapper.GetOneCard(dbCard);
            return appCard;
        }

        public async Task<AppCard> AddOneCard(AppCard card)
        {
            // need to handle duplicate outside
            using var context = new Project2Context(_contextOptions);            
            var newCard = DomainDataMapper.AddOneCard(card);
            await context.Cards.AddAsync(newCard);
            await context.SaveChangesAsync();
            return card;
        }







    }
}
