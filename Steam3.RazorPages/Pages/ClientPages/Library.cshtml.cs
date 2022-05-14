using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class LibraryModel : PageModel
    {
        private readonly IAvalibleGameRepository _avalibleGamesRepository;
        private readonly IGameRepository _gamesRepository;

        public LibraryModel(IGameRepository gamesRepository, IAvalibleGameRepository avalibleGamesRepository)
        {
            _avalibleGamesRepository = avalibleGamesRepository;
            _gamesRepository = gamesRepository;
        }


        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(StaticVariables.Login))
                return RedirectToPage("/ClientPages/Login");
            //Games = _gameRepository.GetGames();
            var avalibleGames = _avalibleGamesRepository.GetGames(StaticVariables.Login);
            Games = _gamesRepository.SearchByUser(avalibleGames);
            return Page();
        }

        public IEnumerable<Game> Games { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
