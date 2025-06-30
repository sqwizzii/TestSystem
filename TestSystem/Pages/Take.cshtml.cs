using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Models;

namespace TestSystem.Pages
{
    //public class TakeModel : PageModel
    //{
    //    private readonly ApplicationDbContext _context;

    //    public TakeModel(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public Test Test { get; set; }

    //    public IActionResult OnGet(int id)
    //    {
    //        Test = _context.Tests
    //            .Include(t => t.Questions)
    //                .ThenInclude(q => q.Answers)
    //            .FirstOrDefault(t => t.Id == id);

    //        if (Test == null)
    //            return NotFound();

    //        return Page();
    //    }

    //    public IActionResult OnPost()
    //    {
    //        // Обробка відповідей (опціонально)
    //        return RedirectToPage("/Index");
    //    }
    //}


    public class TakeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TakeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Test Test { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Test = await _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (Test == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (test == null)
                return NotFound();

            int correct = 0;

            foreach (var question in test.Questions)
            {
                var formAnswers = Request.Form["Answers[" + question.Id + "]"];
                if (formAnswers.ToString() != "")
                {
                    var selected = formAnswers.ToString().Split(',').Select(int.Parse).ToList();

                    var correctAnswers = question.Answers
                        .Where(a => a.IsCorrect)
                        .Select(a => a.Id)
                        .ToList();

                    if (question.Type == QuestionType.SingleChoice)
                    {
                        if (selected.Count == 1 && correctAnswers.Contains(selected.First()))
                            correct++;
                    }
                    else if (question.Type == QuestionType.MultipleChoice)
                    {
                        if (selected.OrderBy(x => x).SequenceEqual(correctAnswers.OrderBy(x => x)))
                            correct++;
                    }
                }
            }

            TempData["Result"] = $"Правильних відповідей: {correct} з {test.Questions.Count}";
            return RedirectToPage("/Result");
        }
    }

}
