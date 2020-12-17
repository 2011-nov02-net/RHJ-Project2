﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.DataAccess.Entities.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project2.Api.DTO;
using Project2.Domain;

namespace Project2.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepo storeRepo, ILogger<UserController> logger, IMapper mapper)
        {
            _logger = logger;
            _userRepo = storeRepo;
            _mapper = mapper;
        }

        //GET /api/users
        //Gets all users
        [HttpGet]      
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> Get()
        {
            // auto mapper, target <- source
            var users = await _userRepo.GetAllUsers();
            if (users != null)
            {
                //var usersReadDTO = _mapper.Map<IEnumerable<UserReadDTO>>(users);
                var usersReadDTO = users.Select(x => new UserReadDTO
                {
                    UserId = x.UserId,
                    First = x.First,
                    Last = x.Last,
                    Email = x.Email,
                    NumPacksPurchased = x.NumPacksPurchased,
                    CurrencyAmount = x.CurrencyAmount,
                });
                return Ok(usersReadDTO);
            }
            return NotFound();           
        }

        //POST /api/users
        //Creates a new user
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Post(UserCreateDTO userCreateDTO  )
        {
            var user = await _userRepo.GetOneUser(userCreateDTO.UserId);
            if (user != null)
            {
                // aleady exist
                return Conflict();
            }

            // 1. for repo
            //var appUser = _mapper.Map<AppUser>(userCreateDTO);
            var appUser = new AppUser
            {
                UserId = userCreateDTO.UserId,
                First = userCreateDTO.First,
                Last = userCreateDTO.Last,
                Email = userCreateDTO.Email,
                UserRole = userCreateDTO.UserRole,
                NumPacksPurchased = userCreateDTO.NumPacksPurchased,
                CurrencyAmount = userCreateDTO.CurrencyAmount,
            };
            await _userRepo.AddOneUser(appUser);

            // 2. for response
            // var userReadDTO = _mapper.Map<UserReadDTO>(appUser);
            var userReadDTO = new UserReadDTO
            {
                UserId = appUser.UserId,
                First = appUser.First,
                Last = appUser.Last,
                Email = appUser.Email,                 
                NumPacksPurchased = appUser.NumPacksPurchased,
                CurrencyAmount = appUser.CurrencyAmount,
            };

            // 3 pieces for POST
            return CreatedAtAction(nameof(GetUserById), new { id = userReadDTO.UserId}, userReadDTO);
        }

        //GET /api/users/{id}
        //Gets a single user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            var user = await _userRepo.GetOneUser(id);
            if (user != null)
            {
                //var userReadDTO = _mapper.Map<UserReadDTO>(user);
                var userReadDTO = new UserReadDTO
                {
                    UserId = user.UserId,
                    First = user.First,
                    Last = user.Last,
                    Email = user.Email,
                    NumPacksPurchased = user.NumPacksPurchased,
                    CurrencyAmount = user.CurrencyAmount,
                };
                return Ok(userReadDTO);
            }
            return NotFound();
        }

        //GET /api/users/{id}/cards
        //Gets a users inventory
        [HttpGet("{id}/cards")]
        public async Task<ActionResult<IEnumerable<CardReadDTO>>> GetUsersInventoryById(string id)
        {
            var userInv = await _userRepo.GetAllCardsOfOneUser(id);
            if (userInv != null)
            {
                //var cardsReadDTO = _mapper.Map<IEnumerable<CardReadDTO>>(userInv);
                var cardsReadDTO = userInv.Select(x => new CardReadDTO
                {
                    CardId = x.CardId,
                    Name = x.Name,
                    Type = x.Type,
                    Rarity = x.Rarity,
                    Value = x.Value,
                });
                return Ok(cardsReadDTO);
            }          
            return NotFound();
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
