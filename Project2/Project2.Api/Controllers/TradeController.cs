using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {

        private readonly ITradeRepo _tradeRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICardRepo _cardRepo;

        public TradeController( ITradeRepo tradeRepo, IUserRepo userRepo, ICardRepo cardRepo)
        {
            _tradeRepo = tradeRepo;
            _userRepo = userRepo;
            _cardRepo = cardRepo;
        }

        //GET /api/trades
        //Gets all trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TradeReadDTO>>> Get()
        {
            var trades = await _tradeRepo.GetAllTrades();
            if (trades != null)
            {
                var tradeDTOs = trades.Select(x => new TradeReadDTO
                {
                    TradeId = x.TradeId,
                    OffererId = x.OffererId,
                    BuyerId = x.BuyerId,
                    TradeDate = x.TradeDate,
                    IsClosed = x.IsClosed,
                    //tradeDetails
                    OfferCardId = x.OfferCardId,
                    BuyerCardId = x.BuyerCardId
                });
                return Ok(tradeDTOs);
            }
            return NotFound();
        }

        //POST /api/trades
        //Creates a trade
        [HttpPost]
        public async Task<ActionResult<TradeCreateDTO>> Post(TradeCreateDTO newTrade)
        {
            //check if trade already exists
            Console.WriteLine("ID: " + newTrade.TradeId);
            //var trade = _tradeRepo.GetOneTrade(newTrade.TradeId);
            TradeReadDTO trade = null;
            if (trade == null)
            {
                AppUser buyer = await _userRepo.GetOneUser(newTrade.BuyerId);
                AppUser offerer = await _userRepo.GetOneUser(newTrade.OffererId);
                AppCard card = await _cardRepo.GetOneCard(newTrade.OfferCardId);
                 var appTrade = new AppTrade()
                 {
                    TradeId = newTrade.TradeId,
                    OffererId = offerer.UserId,
                    //BuyerId = buyer.UserId, dont need when creating a trade only updating
                    TradeDate = newTrade.TradeDate,
                    IsClosed = false, //should always be false when creating
                    //tradeDetails
                    OfferCardId = card.CardId,
                     //BuyerCardId = newTrade.BuyerCardId dont need when creating a trade only updating
                 };
                var tradeDTO = new TradeReadDTO
                {
                    TradeId = newTrade.TradeId,
                    OffererId = newTrade.OffererId,
                    //BuyerId = newTrade.BuyerId,
                    IsClosed = newTrade.IsClosed,
                    TradeDate = newTrade.TradeDate,
                    //TradeDetails
                    OfferCardId = newTrade.TradeId
            };

                await _tradeRepo.AddOneTrade(appTrade);
                return CreatedAtAction(nameof(GetTradeById), new { id = tradeDTO.TradeId }, tradeDTO);
            }

            return Conflict();
        }

        //GET /api/trades/{id}
        //Gets a trade by id
        [HttpGet("{id}")]
        public async Task<ActionResult<TradeReadDTO>> GetTradeById(string id)
        {
            var trade = await _tradeRepo.GetOneTrade(id);
            if (trade != null)
            {
                var tradeDTO = new TradeReadDTO
                {
                    TradeId = trade.TradeId,
                    OffererId = trade.OffererId,
                    BuyerId = trade.BuyerId,
                    TradeDate = trade.TradeDate,
                    IsClosed = trade.IsClosed,
                    //tradeDetails
                    OfferCardId = trade.OfferCardId,
                    BuyerCardId = trade.BuyerCardId
                };
                return Ok(tradeDTO);
            }
            return NotFound();
        }

        //PUT /api/trades/{id}
        //updates auction by id
        [HttpPut("{id}")]
        public async Task<ActionResult<TradeCreateDTO>> UpdateTradeById(string id, TradeCreateDTO tradeDTO)
        {
            var trade = await _tradeRepo.GetOneTrade(tradeDTO.TradeId);
            //update if trade is found
            if (trade != null)
            {
                var appTrade = new AppTrade()
                {
                    TradeId = tradeDTO.TradeId,
                    OffererId = tradeDTO.OffererId,
                    BuyerId = tradeDTO.BuyerId,
                    TradeDate = tradeDTO.TradeDate,
                    IsClosed = tradeDTO.IsClosed,
                    //tradeDetails
                    OfferCardId = tradeDTO.OfferCardId,
                    BuyerCardId = tradeDTO.BuyerCardId
                };

                AppCard offercard = await _cardRepo.GetOneCard(tradeDTO.OfferCardId);
                AppCard buyercard = await _cardRepo.GetOneCard(tradeDTO.BuyerCardId);
                //perform business logic after updating the AppTrade
                if (appTrade.IsClosed && appTrade.BuyerId != null) {
                    appTrade.Buyer = await _userRepo.GetOneUser(appTrade.BuyerId);
                    appTrade.Offerer = await _userRepo.GetOneUser(appTrade.OffererId);
                    appTrade.BuyerCardId = buyercard.CardId;
                    appTrade.OfferCardId = offercard.CardId;
                    //appTrade.MakeTrade();

                    
                    await _userRepo.AddOneCardToOneUser(appTrade.BuyerId, offercard.CardId);
                    await _userRepo.AddOneCardToOneUser(appTrade.OffererId, buyercard.CardId);
                    await _userRepo.DeleteOneCardOfOneUser(appTrade.BuyerId, buyercard.CardId);
                    await _userRepo.DeleteOneCardOfOneUser(appTrade.OffererId, offercard.CardId);
                }
                
                bool result = await _tradeRepo.UpdateTradeById(id, appTrade);
                if (result)
                {
                    return NoContent(); //update successfull
                }
                else
                {
                    return BadRequest(); //something wrong with update
                }
            }

            return NotFound(); //Return 404 if no auction details found

        }


    }
}
