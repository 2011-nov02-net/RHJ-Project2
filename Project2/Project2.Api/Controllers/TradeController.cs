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

        public TradeController( ITradeRepo tradeRepo)
        {
            _tradeRepo = tradeRepo;
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
            var trade = _tradeRepo.GetOneTrade(newTrade.TradeId);
            if (trade == null)
            {
                var appTrade = new AppTrade()
                {
                    TradeId = newTrade.TradeId,
                    OffererId = newTrade.OffererId,
                    BuyerId = newTrade.BuyerId,
                    TradeDate = newTrade.TradeDate,
                    IsClosed = newTrade.IsClosed,
                    //tradeDetails
                    OfferCardId = newTrade.OfferCardId,
                    BuyerCardId = newTrade.BuyerCardId
                };

                await _tradeRepo.AddOneTrade(appTrade);
                return CreatedAtAction("Create Trade", new { appTrade });
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
            var trade = _tradeRepo.GetOneTrade(tradeDTO.TradeId);
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

                bool result = await _tradeRepo.UpdateTradeById(id,  appTrade);
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
