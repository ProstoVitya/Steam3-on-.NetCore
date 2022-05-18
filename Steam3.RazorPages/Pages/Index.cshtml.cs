using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;

namespace Steam3.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGameRepository _db;

        public IndexModel(ILogger<IndexModel> logger, IGameRepository db)
        {
            _logger = logger;
            _db = db;
        }

        public IEnumerable<Game> Games { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            Games = _db.Search(SearchTerm);
        }

        public IActionResult OnPostTest(string gameName)
        {
            return RedirectToPage("NotFound");
        }
    }
}