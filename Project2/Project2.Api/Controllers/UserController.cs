using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //GET /api/users
        //Gets all users
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //POST /api/users
        //Creates a new user
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/users?id=1
        //Gets a single user by id
        [HttpGet()]
        public IActionResult GetUserById([FromQuery] string id = "")
        {
            return Ok();
        }

        //GET /api/users/cards?userid=1
        //Gets a users inventory
        [HttpGet("/cards")]
        public IActionResult GetUsersInventoryById([FromQuery] string userid = "")
        {
            return Ok();
        }

        //POST /api/users/cards?userid=1
        //Creates a card in users inventory
        [HttpPost("/cards")]
        public IActionResult AddCardToUserInventory([FromQuery] string userid = "")
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/users/cards?userid=1&cardid=1
        //Gets a single users card by id
        [HttpGet("/cards")]
        public IActionResult GetUsersCardById([FromQuery] string userid = "", [FromQuery] string cardid = "")
        {
            return Ok();
        }

        //PUT /api/users/cards?userid=1&cardid=1
        //Updates a single users card by id ex. qty user has
        [HttpPut("/cards")]
        public IActionResult UpdateUsersCardById([FromQuery] string userid = "", [FromQuery] string cardid = "")
        {
            return NoContent();
        }

        //DELETE /api/users/cards?userid=1&cardid=1
        //Deletes a single user card by id
        [HttpDelete("/cards")]
        public IActionResult DeleteUsersCardById([FromQuery] string userid = "", [FromQuery] string cardid = "")
        {
            return NoContent();
        }
    }
}
