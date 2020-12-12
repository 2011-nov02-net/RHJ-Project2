using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //GET /api/auctions
        //Gets all auctions
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //POST /api/auctions
        //creates an auction
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/auctions/{id}
        //Gets auction by id
        [HttpGet("{id}")]
        public IActionResult GetAuctionById(string id)
        {
            return Ok();
        }

        //PUT /api/auctions/{id}
        //Update auction by id
        [HttpPut("{id}")]
        public IActionResult UpdateAuctionById(string id)
        {
            return NoContent();
        }

        //GET /api/auctions/{id}/details
        //Gets auction details by id
        [HttpGet("{id}/details")]
        public IActionResult GetAuctionDetailsById(string id)
        {
            return Ok();
        }

        //PUT /api/auctions/{id}/details
        //Update auction details by id
        [HttpPut("{id}/details")]
        public IActionResult UpdateAuctionDetailsById(string id)
        {
            return NoContent();
        }
    }
}
