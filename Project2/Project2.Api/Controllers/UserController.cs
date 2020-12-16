using AutoMapper;
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

        public UserController(ILogger<UserController> logger, IUserRepo storeRepo, IMapper mapper)
        {
            _logger = logger;
            _userRepo = storeRepo;
            _mapper = mapper;
        }

        //GET /api/users
        //Gets all users
        [HttpGet]      
        public ActionResult<IEnumerable<UserReadDTO>> Get()
        {
            // auto mapper, target <- source
            var users = _userRepo.GetAllUsers();
            if (users != null)
            {
                var usersReadDTO = _mapper.Map<IEnumerable<UserReadDTO>>(users);
                return Ok(usersReadDTO);
            }
            return NotFound();           
        }

        //POST /api/users
        //Creates a new user
        [HttpPost]
        public ActionResult<UserReadDTO> Post(UserCreateDTO userCreateDTO  )
        {
            var user = _userRepo.GetOneUser(userCreateDTO.UserId);
            if (user != null)
            {
                // aleady exist
                // not sure about the return type
                return Conflict();
            }
            // 1. map for repo->db
            var appUser = _mapper.Map<AppUser>(userCreateDTO);
            _userRepo.AddOneUser(appUser);  

            // 2. map for response
            var userReadDTO = _mapper.Map<UserReadDTO>(appUser);

            // 3 pieces for POST
            return CreatedAtAction(nameof(GetUserById), new { id = userReadDTO.UserId}, userReadDTO);
        }

        //GET /api/users/{id}
        //Gets a single user by id
        [HttpGet("{id}")]
        public ActionResult<UserReadDTO> GetUserById(string id)
        {
            var user = _userRepo.GetOneUser(id);
            if (user != null)
            {
                var userReadDTO = _mapper.Map<UserReadDTO>(user);
                return Ok(userReadDTO);
            }
            return NotFound();
        }

        //GET /api/users/{id}/cards
        //Gets a users inventory
        [HttpGet("{id}/cards")]
        public ActionResult<IEnumerable<CardReadDTO>> GetUsersInventoryById(string id)
        {
            var userInv = _userRepo.GetAllCardsOfOneUser(id);
            if (userInv != null)
            {
                var cardsReadDTO = _mapper.Map<IEnumerable<CardReadDTO>>(userInv);
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
