using Microsoft.AspNetCore.Mvc;
using TestSystem.Entities;
using TestSystem.Services;

namespace TestSystem.Controllers
{
    [Route("teacher")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // Головна сторінка кабінету вчителя
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // Сторінка створення тесту (GET)
        [HttpGet("create-test")]
        public IActionResult CreateTest()
        {
            return View();
        }

        // Обробка створення тесту (POST)
        [HttpPost("create-test")]
        public async Task<IActionResult> CreateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                await _teacherService.CreateTestAsync(test);
                return RedirectToAction("Index");
            }

            return View(test);
        }

        // Сторінка додавання запитання (GET)
        [HttpGet("add-question/{testId}")]
        public IActionResult AddQuestion(int testId)
        {
            return View(new Question { TestId = testId });
        }

        // Обробка додавання запитання (POST)
        [HttpPost("add-question")]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                await _teacherService.AddQuestionAsync(question);
                return RedirectToAction("AddQuestion", new { testId = question.TestId });
            }

            return View(question);
        }
    }
}
