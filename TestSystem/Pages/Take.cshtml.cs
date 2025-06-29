using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Models;

namespace TestSystem.Pages
{
    public class TakeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TakeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Test Test { get; set; }

        public IActionResult OnGet(int id)
        {
            Test = _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(t => t.Id == id);

            if (Test == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            // Обробка відповідей (опціонально)
            return RedirectToPage("/Index");
        }
    }

}
