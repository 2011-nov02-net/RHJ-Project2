
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
using Project2.DataAccess.Entities.Repo.Interfaces;

namespace Project2.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo storeRepo)
        {        
            _userRepo = storeRepo;   
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
        public async Task<ActionResult<UserReadDTO>> Post(UserCreateDTO userCreateDTO)
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

        //GET /api/users/{email}
        //Gets a single user by email
        [Route("emails/{email}")]
        [HttpGet]
        public async Task<ActionResult<UserReadDTO>> GetUserByEmail(string email)
        {
            var user = await _userRepo.GetOneUserByEmail(email);
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
            var user = await _userRepo.GetOneUser(id);
             
            if (user != null)
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
                        Rating = x.Rating,
                        NumOfRatings = x.NumOfRatings,
                        Image = x.Image,
                    });
                    return Ok(cardsReadDTO);
                }
                return NotFound();                        
            }          
            return NotFound();
        }

        //POST /api/users/{id}/cards
        //Creates a card in users inventory
        [HttpPost("{id}/cards")]
        public async Task<ActionResult<CardReadDTO>> AddCardToUserInventory(string id, CardCreateDTO cardCreateDTO)
        {
            var user = await _userRepo.GetOneUser(id);
            AppCard newCard;
            if (user != null)
            {               
                newCard = new AppCard(cardCreateDTO.Rating, cardCreateDTO.NumOfRatings, cardCreateDTO.Rarity)
                {
                    CardId = cardCreateDTO.CardId,
                    Name = cardCreateDTO.Name,
                    Type = cardCreateDTO.Type,
                    Image = cardCreateDTO.Image,
                };
                newCard.UpdateValue();
                await _userRepo.AddOneCardToOneUser(id,newCard.CardId);            
            }
            else
            {
                return NotFound();
            }

            var cardReadDTO = new CardReadDTO
            {
                CardId = cardCreateDTO.CardId,
                Name = cardCreateDTO.Name,
                Type = cardCreateDTO.Type,
                Rarity = cardCreateDTO.Rarity,
                Rating = cardCreateDTO.Rating,
                NumOfRatings = cardCreateDTO.NumOfRatings,
                Value = cardCreateDTO.Value
            };

            // what method to return
            return CreatedAtAction(nameof(GetUsersCardById), new { id= user.UserId, cardid= newCard.CardId }, cardReadDTO);
        }

        //GET /api/users/{id}/cards/1
        //Gets a single users card by id
        [HttpGet("{id}/cards/{cardid}")]
        
        public async Task<ActionResult<CardReadDTO>> GetUsersCardById(string id, string cardid)
        {
            var user = await _userRepo.GetOneUser(id);
            if (user != null)
            {
                var card = await _userRepo.GetOneCardOfOneUser(id, cardid);
                if (card != null)
                {
                    var cardReadDTO = new CardReadDTO
                    {
                        CardId = card.CardId,
                        Name = card.Name,
                        Type = card.Type,
                        Rarity = card.Rarity,
                        Value = card.Value,
                    };
                    return Ok(cardReadDTO);
                }
                else 
                {
                    return NotFound();
                }
            }
            else 
            {
                return NotFound();
            }
        }
       

        //DELETE /api/users/{id}/cards?cardid=1
        //Deletes a single user card by id
        [HttpDelete("{id}/cards/{cardid}")]
        public async Task<ActionResult> DeleteUsersCardById(string id, string cardid)
        {
            var user = await _userRepo.GetOneUser(id);
            if (user != null)
            {
                try
                {
                    await _userRepo.DeleteOneCardOfOneUser(id, cardid);
                    return NoContent();
                }
                catch (Exception e)
                {
                    // log exception
                    Console.Write(e);
                    return NotFound();
                }
            }
            else 
            {
                return NotFound();
            }
        }

        //PUT /api/users/{id}
        //updates a user 
        [HttpPut("{id}")]
        public async Task<ActionResult<UserCreateDTO>> UpdateUserById(string id, UserCreateDTO userCreateDTO)
        {
            var checkUser = await _userRepo.GetOneUser(id);
            if(checkUser != null)
            {
                AppUser updatedUser = new AppUser()
                {
                    UserId = userCreateDTO.UserId,
                    UserRole = userCreateDTO.UserRole,
                    First = userCreateDTO.First,
                    Last = userCreateDTO.Last,
                    Email = userCreateDTO.Email,
                    NumPacksPurchased = userCreateDTO.NumPacksPurchased,
                    CurrencyAmount = userCreateDTO.CurrencyAmount
                };

                bool result = await _userRepo.UpdateUserById(id, updatedUser);
                if (result)
                {
                    return NoContent(); //update successfull
                }
                else
                {
                    return BadRequest(); //something wrong with update
                }
            }
            return NotFound(); //Return 404 if no auction details found
        }
    }
}
