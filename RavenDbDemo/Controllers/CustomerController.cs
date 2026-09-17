using Microsoft.AspNetCore.Mvc;
using RavenDbDemo.Models;
using RavenDbDemo.Repositories;

namespace RavenDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            var customers = await _repository.GetAllAsync();

            return Ok(customers);
        }

        // GET: api/customer/1-A
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return NotFound(new
                {
                    message = "Customer not found"
                });

            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(
            [FromBody] Customer customer)
        {
            var result = await _repository.CreateAsync(customer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT: api/customer/1-A
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            string id,
            [FromBody] Customer customer)
        {
            var updated = await _repository.UpdateAsync(id, customer);

            if (!updated)
                return NotFound(new
                {
                    message = "Customer not found"
                });

            return NoContent();
        }

        // DELETE: api/customer/1-A
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(new
                {
                    message = "Customer not found"
                });

            return NoContent();
        }
    }
}