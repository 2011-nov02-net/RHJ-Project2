using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
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
        private readonly IOrderRepo _orderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        //GET /api/order
        //Gets all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> Get()
        {
            var orders = await _orderRepo.GetAllOrders();

            if (orders != null)
            {
                var orderDTO = orders.Select(x => new OrderReadDTO
                {
                    OrderId = x.OrderId,
                    UserId = x.OrdererId,
                    Date = x.Date,
                    Total = x.Total
                });

                return Ok(orderDTO); //return all auctions
            }

            return NotFound(); //Return 404 if no auctions found
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
