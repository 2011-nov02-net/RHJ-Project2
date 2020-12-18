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
    public class CardRepo: ICardRepo
    {
        private readonly Project2Context _context;
        public CardRepo( Project2Context context )
        {
            _context  = context ;
        }

        public async Task<IEnumerable<AppCard>> GetAllCards()
        {   
            var dbCards = await _context.Cards.ToListAsync();
            if (dbCards == null) return null;           
            var appCards = DomainDataMapper.GetAllCards(dbCards); 
            return appCards;
        }

        public async Task<AppCard> GetOneCard(string id)
        {   
            var dbCard = await _context.Cards.FirstOrDefaultAsync(x => x.CardId == id);
            if (dbCard == null) return null;
            var appCard = DomainDataMapper.GetOneCard(dbCard);
            return appCard;
        }

        // need to handle duplicate outside
        public async Task AddOneCard(AppCard card)
        {                         
            var newCard = DomainDataMapper.AddOneCard(card);
            await _context.Cards.AddAsync(newCard);
            await _context.SaveChangesAsync();            
        }
    }
}
