using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Entities;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    [Route("teacher")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===== Головна сторінка вчителя =====
        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View("TeacherDashboard");
        }

        // [1] Список усіх тестів
        [HttpGet("tests")]
        public async Task<IActionResult> AllTests()
        {
            var tests = await _context.Tests.ToListAsync();
            return View("AllTests", tests);
        }

        // [2] Створити тест (GET)
        [HttpGet("create-test")]
        public IActionResult CreateTest()
        {
            return View();
        }

        // [2] Створити тест (POST) ✅ Додано за твоїм запитом
        [HttpPost("create-test")]
        public async Task<IActionResult> CreateTest(Test test)
        {
            if (!ModelState.IsValid)
                return View(test);

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            Console.WriteLine($"✅ Тест \"{test.Title}\" збережено в БД!");

            return RedirectToAction("AllTests");
        }


        // [3] Додати питання до тесту
        [HttpGet("add-question/{testId}")]
        public IActionResult AddQuestion(int testId)
        {
            ViewBag.TestId = testId;
            return View();
        }

        [HttpPost("add-question")]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TestId = question.TestId;
                return View(question);
            }

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewTest", new { id = question.TestId });
        }

        // [4] Додати відповідь до запитання
        [HttpGet("add-answer/{questionId}")]
        public IActionResult AddAnswer(int questionId)
        {
            var model = new AnswerViewModel
            {
                QuestionId = questionId
            };
            return View(model);
        }

        [HttpPost("add-answer")]
        public async Task<IActionResult> AddAnswer(AnswerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var answer = new Answer
            {
                QuestionId = model.QuestionId,
                Text = model.Text,
                IsCorrect = model.IsCorrect
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == model.QuestionId);
            return RedirectToAction("ViewTest", new { id = question.TestId });
        }

        // [5] Перегляд тесту
        [HttpGet("view-test/{id}")]
        public async Task<IActionResult> ViewTest(int id)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (test == null)
                return NotFound();

            return View("ViewTest", test);
        }
    }
}
