using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
using Project2.Domain;
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

                return Ok(orderDTO); //return all orders
            }

            return NotFound(); //Return 404 if no orders found
        }

        //GET /api/order
        //Creates an order
        [HttpPost]
        public async Task<ActionResult<OrderCreateDTO>> Post(OrderCreateDTO newOrder, int qty)
        {
            //check if order exists
            var check = _orderRepo.GetOneOrder(newOrder.OrderId);
            if (check == null)
            {
                var createdOrder = new AppOrder()
                {
                    //order
                    OrderId = newOrder.OrderId,
                    OrdererId = newOrder.UserId,
                    Date = newOrder.Date,
                    Total = newOrder.Total,

                    //order details
                    PackId = newOrder.PackId,
                    PackQty = qty // pack qty saved in order detail db table
                };

                await _orderRepo.AddOneOrder(qty, createdOrder);

                return CreatedAtAction("Create Order", new { createdOrder }); //201 new order created
            }

            return Conflict(); //order already exists and cant be created
        }

        //GET /api/order/{id}
        //Get an order by id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDTO>> GetOrderById(string id)
        {
            var order = await _orderRepo.GetOneOrder(id);

            if (order != null)
            {
                var orderDTO = new OrderReadDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.OrdererId,
                    Date = order.Date,
                    Total = order.Total
                };

                return Ok(orderDTO); //success return order
            }

            return NotFound(); //Return 404 if no order found
        }

        //GET /api/order/{id}/details
        //Gets an orders details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetOrderDetailsById(string id)
        {
            var items = await _orderRepo.GetOneOrderDetail(id);

            if (items != null)
            {
                var orderItemDTO = items.Select(x => new OrderReadDTO
                {
                    OrderId = id,
                    PackId = x.PackId,
                    PackQty = x.PackQty //pack qty saved in order detail db table
                });

                return Ok(orderItemDTO); //return all orders
            }

            return NotFound(); //Return 404 if no orders found
        }
    }
}
