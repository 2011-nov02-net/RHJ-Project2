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
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepo _storeRepo;

        public StoreController(IStoreRepo storeRepo)
        {
            _storeRepo = storeRepo;
        }

        //GET /api/store
        //Gets all items in store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppStoreItem>>> Get()
        {
            var storeItems = await _storeRepo.GetAllStoreItems();
            if (storeItems != null)
            {
                var storeDTOs = storeItems.Select(x => new StoreDTO
                {
                    PackId = x.PackId,
                    PackQty = x.Qty
                });
                return Ok(storeDTOs);
            }
            return NotFound();
        }

        //GET /api/store/1
        //Gets a store item by id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppStoreItem>> GetStoreItemById(string id)
        {
            var storeItem = await _storeRepo.GetStoreItemById(id);
            if (storeItem != null)
            {
                var storeDTO = new StoreDTO
                {
                    PackId = storeItem.PackId,
                    PackQty = storeItem.Qty
                };
                return Ok(storeDTO);
            }
            return NotFound();
        }

        //PUT /api/store?id=1
        //Update a store item by id
        [HttpPut("{id}")]
        public async Task<ActionResult<AppStoreItem>> UpdateStoreItemById(string id, StoreDTO storeDTO)
        {
            var storeItem = await _storeRepo.GetStoreItemById(id);
            //update if Item is found 
            if (storeItem != null)
            {
                //only need to update item quantity
                bool result = await _storeRepo.UpdateStoreItemById(id, storeDTO.PackQty);
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
