using Microsoft.AspNetCore.Mvc;
using TestSystem.Entities;
using TestSystem.Services;
using static System.Net.Mime.MediaTypeNames;

namespace TestSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Test>>> GetAll()
        {
            return await _testService.GetAllTestsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> Get(int id)
        {
            var test = await _testService.GetTestByIdAsync(id);
            if (test == null) return NotFound();
            return test;
        }

        [HttpPost]
        public async Task<ActionResult<Test>> Create(Test test)
        {
            var created = await _testService.CreateTestAsync(test);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Test test)
        {
            if (id != test.Id) return BadRequest();
            await _testService.UpdateTestAsync(test);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _testService.DeleteTestAsync(id);
            return NoContent();
        }
    }
}
