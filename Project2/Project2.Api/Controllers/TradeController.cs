using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //GET /api/trades
        //Gets all trades
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //POST /api/trades
        //Creates a trade
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/trades/{id}
        //Gets a trade by id
        [HttpGet("{id}")]
        public IActionResult GetTradeById(string id)
        {
            return Ok();
        }

        //GET /api/trades/{id}/details
        //Gets a trade by id
        [HttpGet("{id}/details")]
        public IActionResult GetTradeDetailsById(string id)
        {
            return Ok();
        }
    }
}
