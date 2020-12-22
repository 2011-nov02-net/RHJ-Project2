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
    [Route("api/auctions")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepo _auctRepo;

        public AuctionController(IAuctionRepo auctRepo)
        {
            _auctRepo = auctRepo;
        }

        //GET /api/auctions
        //Gets all auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionReadDTO>>> Get()
        {
            var auctions = await _auctRepo.GetAllAuctions();

            if (auctions != null)
            {

                var auctionDTO = auctions.Select(x => new AuctionReadDTO
                {
                    //auction
                    AuctionId = x.AuctionId,
                    SellerId = x.SellerId,
                    BuyerId = x.BuyerId,
                    CardId = x.CardId,
                    PriceSold = (double)x.PriceSold,
                    SellDate = (DateTime)x.SellDate,

                    //auctionDetails
                    PriceListed = x.PriceListed,
                    BuyoutPrice = x.BuyoutPrice,
                    NumberBids = (int)x.NumberBids,
                    SellType = x.SellType,
                    ExpDate = x.ExpDate

                });

                return Ok(auctionDTO); //return all auctions
            }

            return NotFound(); //Return 404 if no auctions found
        }

        //POST /api/auctions
        //creates an auction
        [HttpPost]
        public async Task<ActionResult<AuctionCreateDTO>> Post(AuctionCreateDTO newAuction)
        {
            //check if auction exists
            var check = await _auctRepo.GetAuctionById(newAuction.AuctionId);
            if(check == null)
            {
                //for repo
                var createdAuction = new AppAuction()
                {
                    // auction
                    AuctionId = newAuction.AuctionId,
                    SellerId = newAuction.SellerId,
                    BuyerId = newAuction.BuyerId,
                    CardId = newAuction.CardId,
                    PriceSold = (double)newAuction.PriceSold,
                    SellDate = (DateTime)newAuction.SellDate,

                    //auction details
                    PriceListed = newAuction.PriceListed,
                    BuyoutPrice = newAuction.BuyoutPrice,
                    NumberBids = newAuction.NumberBids,
                    SellType = newAuction.SellType,
                    ExpDate = newAuction.ExpDate

                };
                await _auctRepo.CreateAuction(createdAuction);

                //for response
                var auctionReadDTO = new AuctionReadDTO
                {
                    //auction
                    AuctionId = createdAuction.AuctionId,
                    SellerId = createdAuction.SellerId,
                    BuyerId = createdAuction.BuyerId,
                    CardId = createdAuction.CardId,
                    PriceSold = (double)createdAuction.PriceSold,
                    SellDate = (DateTime)createdAuction.SellDate,

                    //auction details
                    PriceListed = createdAuction.PriceListed,
                    BuyoutPrice = createdAuction.BuyoutPrice,
                    NumberBids = (int)createdAuction.NumberBids,
                    SellType = createdAuction.SellType,
                    ExpDate = createdAuction.ExpDate
                };

                return CreatedAtAction(nameof(GetAuctionById), new {id = auctionReadDTO.AuctionId},auctionReadDTO); //201 new auction created
            }

            return Conflict(); //auction already exists and cant be created
        }

        //GET /api/auctions/{id}
        //Gets auction by id
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionReadDTO>> GetAuctionById(string id)
        {
            var auction = await _auctRepo.GetAuctionById(id);

            if (auction != null)
            {
                var auctionDTO = new AuctionReadDTO
                {
                    AuctionId = auction.AuctionId,
                    SellerId = auction.SellerId,
                    BuyerId = auction.BuyerId,
                    CardId = auction.CardId,
                    PriceSold = (double)auction.PriceSold,
                    SellDate = (DateTime)auction.SellDate,
                    //for auction details
                    PriceListed = auction.PriceListed,
                    BuyoutPrice = auction.BuyoutPrice,
                    NumberBids = (int)auction.NumberBids,
                    SellType = auction.SellType,
                    ExpDate = auction.ExpDate

                };

                return Ok(auctionDTO); //return auction
            }

            return NotFound(); //Return 404 if no auction found
        }

        //PUT /api/auctions/{id}
        //Update auction by id
        [HttpPut("{id}")]
        public async Task<ActionResult<AuctionCreateDTO>> UpdateAuctionById(string id, AuctionCreateDTO newAuction)
        {
            var check = await _auctRepo.GetAuctionById(id);

            if (check != null)
            {
                var auction = new AppAuction
                {
                    AuctionId = newAuction.AuctionId,
                    SellerId = newAuction.SellerId,
                    BuyerId = newAuction.BuyerId,
                    CardId = newAuction.CardId,
                    PriceSold = (double)newAuction.PriceSold,
                    SellDate = (DateTime)newAuction.SellDate,
                    //for auction details
                    PriceListed = newAuction.PriceListed,
                    BuyoutPrice = newAuction.BuyoutPrice,
                    NumberBids = (int)newAuction.NumberBids,
                    SellType = newAuction.SellType,
                    ExpDate = newAuction.ExpDate

                };
                bool result = await _auctRepo.UpdateAuction(id, auction);
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

        //GET /api/auctions/{id}/details
        //Gets auction details by id
/*        [HttpGet("{id}/details")]
        public async Task<ActionResult<AuctionReadDTO>> GetAuctionDetailsById(string id)
        {
            var details = await _auctRepo.GetAuctionDetailById(id);

            if (details != null)
            {
                var auctionDTO = new AuctionReadDTO
                {
                    AuctionId = details.AuctionId,
                    PriceListed = details.PriceListed,
                    BuyoutPrice = details.BuyoutPrice,
                    NumberBids = (int)details.NumberBids,
                    SellType = details.SellType,
                    ExpDate = details.ExpDate

                };

                return Ok(auctionDTO); //return auction details
            }

            return NotFound(); //Return 404 if no auction details found
        }*/

        //PUT /api/auctions/{id}/details
        //Update auction details by id
/*        [HttpPut("{id}/details")]
        public async Task<ActionResult<AuctionCreateDTO>> UpdateAuctionDetailsById(string id, AuctionCreateDTO newAuction)
        {
            var check = await _auctRepo.GetAuctionDetailById(id);

            if (check != null)
            {
                var auction = new AppAuction
                {
                    AuctionId = newAuction.AuctionId,
                    PriceListed = newAuction.PriceListed,
                    BuyoutPrice = newAuction.BuyoutPrice,
                    NumberBids = newAuction.NumberBids,
                    SellType = newAuction.SellType,
                    ExpDate = newAuction.ExpDate

                };
                bool result = await _auctRepo.UpdateAuctionDetail(id, auction);
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
        }*/
    }
}
