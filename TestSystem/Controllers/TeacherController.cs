using Microsoft.AspNetCore.Mvc;
using TestSystem.Data;
using TestSystem.Entities;

using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Entities;

namespace TestApp.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /teacher/tests
        [HttpGet("/teacher/tests")]
        public async Task<IActionResult> AllTests()
        {
            var tests = await _context.Tests.ToListAsync();
            return View("AllTests", tests);
        }

        // GET: /teacher/create-test
        [HttpGet("/teacher/create-test")]
        public IActionResult CreateTest()
        {
            return View();
        }

        // POST: /teacher/create-test
        [HttpPost("/teacher/create-test")]
        public async Task<IActionResult> CreateTest(Test test)
        {
            if (!ModelState.IsValid) return View(test);

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllTests");
        }

        // GET: /teacher/add-question/{testId}
        [HttpGet("/teacher/add-question/{testId}")]
        public IActionResult AddQuestion(int testId)
        {
            ViewBag.TestId = testId;
            return View(); // створиш свою сторінку
        }
    }
}
