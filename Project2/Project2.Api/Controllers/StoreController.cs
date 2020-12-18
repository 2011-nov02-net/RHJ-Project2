using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        //GET /api/store
        //Gets all items in store
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //GET /api/store/1
        //Gets a store item by id
        [HttpGet("{id}")]
        public IActionResult GetStoreItemById(string id)
        {
            return Ok();
        }

        //PUT /api/store?id=1
        //Update a store item by id
        [HttpPut()]
        public IActionResult UpdateStoreItemById([FromQuery] string id = "")
        {
            return Ok();
        }
    }
}
