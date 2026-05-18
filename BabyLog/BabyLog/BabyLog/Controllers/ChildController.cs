using BabyLog.Models;
using BabyLog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BabyLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildRepository _childRepository;

        public ChildController(IChildRepository childRepository)
        {
            _childRepository = childRepository;
        }

        // Gets all children connected to one customer
        // Example: GET api/child/customer/1
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Child>>> GetChildrenByCustomer(int customerId)
        {
            var children = await _childRepository.GetChildrenByCustomerIdAsync(customerId);

            return Ok(children);
        }

        // Gets one child by ChildId
        // Example: GET api/child/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChild(int id)
        {
            var child = await _childRepository.GetByIdAsync(id);

            // Child was not found
            if (child == null)
            {
                return NotFound();
            }

            return Ok(child);
        }

        // Creates a new child profile using stored procedure
        // Example: POST api/child
        [HttpPost]
        public async Task<ActionResult<Child>> CreateChild(Child child)
        {
            // Set creation date automatically
            child.ChildCreatedDate = DateTime.UtcNow;

            await _childRepository.CreateChildAsync(child);

            return Ok(child);
        }

        // Updates an existing child profile using stored procedure
        // Example: PUT api/child/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Child>> UpdateChild(int id, Child updatedChild)
        {
            var child = await _childRepository.GetByIdAsync(id);

            // Child was not found
            if (child == null)
            {
                return NotFound();
            }

            // Make sure route id is used
            updatedChild.ChildId = id;

            await _childRepository.UpdateChildAsync(updatedChild);

            return Ok(updatedChild);
        }

        // Deletes a child profile using stored procedure
        // Example: DELETE api/child/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var child = await _childRepository.GetByIdAsync(id);

            // Child was not found
            if (child == null)
            {
                return NotFound();
            }

            await _childRepository.DeleteChildAsync(id);

            return Ok();
        }
    }
}