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

        //GET /api/users/{id}
        //Gets a single user by id
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            return Ok();
        }

        //GET /api/users/{id}/cards
        //Gets a users inventory
        [HttpGet("{id}/cards")]
        public IActionResult GetUsersInventoryById(string id)
        {
            return Ok();
        }

        //POST /api/users/{id}/cards
        //Creates a card in users inventory
        [HttpPost("{id}/cards")]
        public IActionResult AddCardToUserInventory(string id)
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/users/{id}/cards?cardid=1
        //Gets a single users card by id
        [HttpGet("{id}/cards")]
        public IActionResult GetUsersCardById(string id, [FromQuery] string cardid = "")
        {
            return Ok();
        }

        //PUT /api/users/{id}/cards?cardid=1
        //Updates a single users card by id ex. qty user has
        [HttpPut("{id}/cards")]
        public IActionResult UpdateUsersCardById(string id, [FromQuery] string cardid = "")
        {
            return NoContent();
        }

        //DELETE /api/users/{id}/cards?cardid=1
        //Deletes a single user card by id
        [HttpDelete("{id}/cards")]
        public IActionResult DeleteUsersCardById(string id, [FromQuery] string cardid = "")
        {
            return NoContent();
        }
    }
}
