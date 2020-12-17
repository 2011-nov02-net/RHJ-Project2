using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
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
        private readonly ILogger<PackController> _logger;
        private readonly IPackRepo _packRepo;

        public PackController(IPackRepo packRepo, ILogger<PackController> logger)
        {
            _packRepo = packRepo;
            _logger = logger;
        }

        //GET /api/packs
        //Gets all packs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackReadDTO>>> Get()
        {
            var packs = await _packRepo.GetAllPacks();

            if (packs != null)
            {
                var packsDTO = packs.Select(x => new PackReadDTO
                {
                    PackId = x.PackId,
                    Name = x.Name,
                    Price = x.Price,
                    DateReleased = x.DateReleased
                });

                return Ok(packsDTO); //success
            }

            return NotFound(); //error couldnt find any packs
        }

        //GET /api/packs?id=1
        //Gets a pack by id
        [HttpGet()]
        public async Task<ActionResult<PackReadDTO>> GetPackById([FromQuery] string id = "")
        {
            var pack = await _packRepo.GetOnePack(id);

            if (pack != null)
            {
                var packDTO = new PackReadDTO
                {
                    PackId = pack.PackId,
                    Name = pack.Name,
                    Price = pack.Price,
                    DateReleased = pack.DateReleased
                };

                return Ok(packDTO); //success
            }

            return NotFound(); //error couldnt find any card
        }
    }
}
