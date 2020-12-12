using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/packs")]
    [ApiController]
    public class PackController : ControllerBase
    {
        //GET /api/packs
        //Gets all packs
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //GET /api/packs?id=1
        //Gets a pack by id
        [HttpGet()]
        public IActionResult GetPackById([FromQuery] string id = "")
        {
            return Ok();
        }

        //PUT /api/packs?id=1
        //Update a pack by id, maybe to change price or something
        [HttpPut()]
        public IActionResult UpdatePackById([FromQuery] string id = "")
        {
            return NoContent();
        }
    }
}
