using Microsoft.EntityFrameworkCore;
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

            // can be replaced by mapper
            var appCards = dbCards.Select(x => new AppCard
            {
                CardId = x.CardId,
                Name = x.Name,
                Type = x.Type,
                Rarity = x.Rarity,
                Value = x.Value,
            });
            //var appCards = DomainDataMapper.GetAllCards(dbCards); 
            return appCards;
        }

        public async Task<AppCard> GetOneCard(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbCard = await context.Cards.FirstOrDefaultAsync(x => x.CardId == id);
            if (dbCard == null) return null;

            // can be replaced by mapper
            var appCard = new AppCard
            {
                CardId = dbCard.CardId,
                Name = dbCard.Name,
                Type = dbCard.Type,
                Rarity = dbCard.Rarity,
                Value = dbCard.Value,
            };
            //var appCard = DomainDataMapper.GetOneCard(dbCard);
            return appCard;
        }

        public async Task AddOneCard(AppCard card)
        {
            using var context = new Project2Context(_contextOptions);

            // can be replaced by mapper
            var newCard = new Card
            {
                CardId = card.CardId,
                Name = card.Name,
                Type = card.Type,
                Rarity = card.Rarity,
                Value = card.Value,
            };
            //var newCard = DomainDataMapper.AddOneCard(card);
            await context.Cards.AddAsync(newCard);
            await context.SaveChangesAsync();
        }







    }
}
