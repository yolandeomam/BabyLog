using BabyLog.Models;
using BabyLog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BabyLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SleepController : ControllerBase
    {
        private readonly ISleepRepository _sleepRepository;

        public SleepController(ISleepRepository sleepRepository)
        {
            _sleepRepository = sleepRepository;
        }

        // Gets all sleep registrations for one child
        // Example: GET api/sleep/child/1
        [HttpGet("child/{childId}")]
        public async Task<ActionResult<List<Sleep>>> GetSleepByChild(int childId)
        {
            var sleeps = await _sleepRepository.GetSleepByChildIdAsync(childId);

            return Ok(sleeps);
        }

        // Gets one sleep registration
        // Example: GET api/sleep/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sleep>> GetSleep(int id)
        {
            var sleep = await _sleepRepository.GetByIdAsync(id);

            if (sleep == null)
            {
                return NotFound();
            }

            return Ok(sleep);
        }

        // Creates a new sleep registration
        // Example: POST api/sleep
        [HttpPost]
        public async Task<ActionResult<Sleep>> CreateSleep(Sleep sleep)
        {
            await _sleepRepository.CreateSleepAsync(sleep);

            return Ok(sleep);
        }

        // Updates an existing sleep registration
        // Example: PUT api/sleep/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Sleep>> UpdateSleep(int id, Sleep updatedSleep)
        {
            var sleep = await _sleepRepository.GetByIdAsync(id);

            if (sleep == null)
            {
                return NotFound();
            }

            updatedSleep.SleepId = id;

            await _sleepRepository.UpdateSleepAsync(updatedSleep);

            return Ok(updatedSleep);
        }

        // Deletes a sleep registration
        // Example: DELETE api/sleep/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSleep(int id)
        {
            var sleep = await _sleepRepository.GetByIdAsync(id);

            if (sleep == null)
            {
                return NotFound();
            }

            await _sleepRepository.DeleteSleepAsync(id);

            return Ok();
        }
    }
}