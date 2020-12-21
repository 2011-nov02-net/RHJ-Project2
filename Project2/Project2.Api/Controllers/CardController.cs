using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepo _cardRepo;

        public CardController(ICardRepo cardRepo)
        {
            _cardRepo = cardRepo;
        }

        //GET /api/cards
        //Gets all cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardReadDTO>>> Get()
        {
            var cards = await _cardRepo.GetAllCards();
            if(cards != null)
            {
                var cardsDTO = cards.Select(x => new CardReadDTO
                {
                    CardId = x.CardId,
                    Name = x.Name,
                    Type = x.Type,
                    Rarity = x.Rarity,
                    Rating = x.Rating,
                    NumOfRatings = x.NumOfRatings,
                    Value = x.Value
                });
                return Ok(cardsDTO); //success
            }
            return NotFound(); //error couldnt find cards
        }

        //POST /api/cards
        //creates a new card
        [HttpPost]
        public async Task<ActionResult<CardReadDTO>> Post(CardCreateDTO newCard)
        {
            //check if card exists
            var card = await _cardRepo.GetOneCard(newCard.CardId);
            if (card != null)
            {
                return Conflict(); //card already exists and cant be created
            }
            var createdCard = new AppCard(newCard.Rating, newCard.NumOfRatings, newCard.Rarity)
            {
                CardId = newCard.CardId,
                Name = newCard.Name,
                Type = newCard.Type,
            };
            await _cardRepo.AddOneCard(createdCard);
            return CreatedAtAction(nameof(GetCardById), new { id = createdCard.CardId }, createdCard); //201 new card created
            
        }

        //GET /api/cards/1
        //Gets a card by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CardReadDTO>> GetCardById(string id)
        {
            var card = await _cardRepo.GetOneCard(id);
            if (card != null)
            {
                var cardDTO = new CardReadDTO
                {
                    CardId = card.CardId,
                    Name = card.Name,
                    Type = card.Type,
                    Rarity = card.Rarity,
                    Rating = card.Rating,
                    NumOfRatings = card.NumOfRatings,
                    Value = card.Value
                };
                return Ok(cardDTO); //success
            }
            return NotFound(); //error couldn't find any card
        }
    }
}
