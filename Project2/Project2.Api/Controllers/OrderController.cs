using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //GET /api/order
        //Gets all orders
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //GET /api/order
        //Creates an order
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(); //CreatedAtAction();
        }

        //GET /api/order/{id}
        //Get an order by id
        [HttpGet("{id}")]
        public IActionResult GetOrderById(string id)
        {
            return Ok();
        }

        //GET /api/order/{id}/details
        //Gets an orders details
        [HttpGet("{id}/details")]
        public IActionResult GetOrderDetailsById(string id)
        {
            return Ok();
        }
    }
}
