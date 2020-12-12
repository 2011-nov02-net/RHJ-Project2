using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.DataAccess.Entities.Repo
{
    // will start mapping later
    public static class DomainDataMapper
    {
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

        public static IEnumerable<AppCard> GetAllCards( IEnumerable<Card> dbCards)
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

    }
}
