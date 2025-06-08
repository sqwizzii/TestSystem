using Microsoft.AspNetCore.Mvc;
using TestSystem.Entities;

[Route("teacher")]
public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet("create-test")]
    public IActionResult CreateTest() => View();

    [HttpPost("create-test")]
    public async Task<IActionResult> CreateTest(Test test)
    {
        await _teacherService.CreateTestAsync(test);
        return RedirectToAction("CreateTest");
    }

    [HttpGet("add-question/{testId}")]
    public IActionResult AddQuestion(int testId) => View(new Question { TestId = testId });

    [HttpPost("add-question")]
    public async Task<IActionResult> AddQuestion(Question question)
    {
        await _teacherService.AddQuestionAsync(question);
        return RedirectToAction("AddQuestion", new { testId = question.TestId });
    }
}
