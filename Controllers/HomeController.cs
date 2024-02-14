using api2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("{id, companyName, contactName}")]
        public IActionResult CreateCustomer(string id, string companyName, string contactName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = _customerService.CreateCustomer(id, companyName, contactName);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id, companyName, contactName}")]
        public IActionResult UpdateCustomer(string id, string companyName, string contactName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var customer = _customerService.UpdateCustomer(id, companyName, contactName);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var result = _customerService.DeleteCustomer(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}