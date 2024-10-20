using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces;
using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;
using IndustryConnect_Week5_WebApi.ApplicationTier.Classes;
using Microsoft.AspNetCore.JsonPatch;

namespace IndustryConnect_Week5_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreMethods _storeMethods;
        public StoreController(IStoreMethods storeMethods)
        {
            _storeMethods = storeMethods;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagedDtos<StoreDto>>>> GetStores(int pageNumber, int pageSize)
        {
            try
            {
                var pagedResult = await _storeMethods.GetAllStoresAsync(pageNumber, pageSize);
                if (pagedResult == null)
                {
                    return NotFound();
                }

                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStore(int id)
        {
            try
            {
                var storeDto = await _storeMethods.GetStoreAsync(id);
                if (storeDto == null)
                {
                    return NotFound();
                }
                return storeDto;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Store/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<StoreDto>> Put(int id, StoreDto? storeDto)
        {
            try
            {
                if (storeDto == null)
                {
                    return BadRequest("Provide some value for Store.");
                }

                storeDto = await _storeMethods.UpdateStoreAsync(id, storeDto);
                return Ok(storeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Store
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreDto>> Post(StoreDto? storeDto)
        {
            try
            {
                if (storeDto == null)
                {
                    return BadRequest("Give proper values for Store.");
                }

                storeDto = await _storeMethods.AddStoreAsync(storeDto);
                return Created("", storeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<StoreDto>> Patch(int id, [FromBody] JsonPatchDocument<StoreDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                {
                    return BadRequest("No values were send to change");
                }

                var storeDto = await _storeMethods.GetStoreAsync(id);

                if (storeDto == null)
                {
                    return BadRequest($"Store with ID {id} was not found.");
                }

                patchDto.ApplyTo(storeDto);

                storeDto = await _storeMethods.PatchStoreDetails(id, storeDto);

                return Ok(storeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteStore(int id)
        {
            try
            {
                var status = await _storeMethods.DeleteStoreAsync(id);

                if (status == StatusEnum.NoContent)
                {
                    return $"Store with Id: {id} deleted successfully!!!";
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
