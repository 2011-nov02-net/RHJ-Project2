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
        private readonly IPackRepo _packRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICardRepo _cardRepo;

        public OrderController(IOrderRepo orderRepo,IPackRepo packRepo,IUserRepo userRepo, ICardRepo cardRepo)
        {
            _orderRepo = orderRepo;
            _packRepo = packRepo;
            _userRepo = userRepo;
            _cardRepo = cardRepo;
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

        //POST /api/order
        //Creates an order
        [HttpPost]
        public async Task<ActionResult<OrderCreateDTO>> Post(OrderCreateDTO newOrder, int qty)
        {
            //check if order exists
            var check = await _orderRepo.GetOneOrder(newOrder.OrderId);
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

                //execute any business logic associated with the order
                createdOrder.Pack = await _packRepo.GetOnePack(createdOrder.PackId);
                createdOrder.Orderer = await _userRepo.GetOneUser(createdOrder.OrdererId);
                createdOrder.CalculateTotal();
                //createdOrder.FillOrder();

                //should just call fillOrder(), will modify later
                Random rand = new Random();
                if (createdOrder.Orderer.CurrencyAmount >= createdOrder.Total)
                {
                    createdOrder.Orderer.CurrencyAmount -= createdOrder.Total;
                    int cards = 8 * createdOrder.PackQty;
                    for (int i = 0; i < cards; ++i)
                    {
                        string cardId = Convert.ToString(rand.Next(64)); //only grabs from the first 64 cards in the set, PackId = base set number
                        AppCard card = await _orderRepo.GetCardFromApi(cardId, createdOrder.PackId);
                        createdOrder.Orderer.AddCardToInventory(card); //add to the AppUser
                        await _cardRepo.AddOneCard(card); //add to card table
                        await _userRepo.AddOneCardToOneUser(createdOrder.OrdererId, card.CardId);
                    }
                }
                else
                    throw new Exception("User funds insufficient.");
                await _userRepo.UpdateUserById(createdOrder.OrdererId, createdOrder.Orderer);
                await _orderRepo.AddOneOrder(qty, createdOrder);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder); //201 new order created
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
