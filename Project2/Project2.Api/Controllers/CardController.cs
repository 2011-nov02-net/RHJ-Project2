using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //GET /api/cards
        //Gets all cards
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //POST /api/cards
        //creates a new card
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/cards?id=1
        //Gets a card by id
        [HttpGet()]
        public IActionResult GetCardById([FromQuery] string id = "")
        {
            return Ok();
        }
    }
}
