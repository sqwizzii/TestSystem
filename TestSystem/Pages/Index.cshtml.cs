using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestSystem.Data;
using TestSystem.Models;

namespace TestSystem.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public List<Test> Tests { get; set; }

        public void OnGet()
        {
            Tests = _context.Tests.ToList();
        }
    }

}
