using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IndustryConnect_Week5_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerMethods _customerMethods;

        public CustomerController(ICustomerMethods customerMethods)
        {
            _customerMethods = customerMethods;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagedDtos<CustomerDto>>>> GetAllCustomer(int pageNumber, int pageSize)
        {
            try
            {
                var pagedResult = await _customerMethods.GetAllCustomersAsync(pageNumber, pageSize);
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

        // GET: api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            try
            {
                var customerDto = await _customerMethods.GetCustomerAsync(id);
                if (customerDto == null)
                {
                    return NotFound();
                }
                return customerDto;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Post([FromBody] CustomerDto customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest("Give proper values for Customer.");
                }

                customerDto = await _customerMethods.AddCustomerAsync(customerDto);
                return Created("", customerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Put(int id, CustomerDto? customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest("Provide some value for Customer");
                }

                customerDto = await _customerMethods.UpdateCustomerAsync(id, customerDto);
                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CustomerDto>> Patch(int id, [FromBody] JsonPatchDocument<CustomerDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                {
                    return BadRequest("No values were send to change");
                }

                var customerDto = await _customerMethods.GetCustomerAsync(id);

                if (customerDto == null)
                {
                    return BadRequest($"Customer with ID {id} was not found.");
                }

                patchDto.ApplyTo(customerDto);

                customerDto = await _customerMethods.PatchCustomerDetails(id, customerDto);

                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var status = await _customerMethods.DeleteCustomerAsync(id);

                if (status == StatusEnum.NoContent)
                {
                    return $"Customer with Id: {id} deleted successfully!!!";
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
