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
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepo _cardRepo;

        public CardController(ICardRepo cardRepo, ILogger<CardController> logger)
        {
            _cardRepo = cardRepo;
            _logger = logger;
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
                    Value = x.Value
                });

                return Ok(cardsDTO); //success
            }

            return NotFound(); //error couldnbt find any cards
        }

        //POST /api/cards
        //creates a new card
        [HttpPost]
        public async Task<ActionResult<CardCreateDTO>> Post(CardCreateDTO newCard)
        {
            //check if card exists
            var check = _cardRepo.GetOneCard(newCard.CardId);
            if (check == null)
            {
                var createdCard = new AppCard()
                {
                    CardId = newCard.CardId,
                    Name = newCard.Name,
                    Type = newCard.Type,
                    Rarity = newCard.Rarity,
                    Value = newCard.Value
                };

                await _cardRepo.AddOneCard(createdCard);

                return CreatedAtAction("Create Card", new { createdCard }); //201 new card created
            }

            return Conflict(); //card already exists and cant be created
        }

        //GET /api/cards?id=1
        //Gets a card by id
        [HttpGet()]
        public async Task<ActionResult<CardReadDTO>> GetCardById([FromQuery] string id = "")
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
                    Value = card.Value
                };

                return Ok(cardDTO); //success
            }

            return NotFound(); //error couldnbt find any cards
        }
    }
}
