using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;
using Steam3.Services.Interfaces;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class LibraryModel : PageModel
    {
        private readonly IAvalibleGameRepository _avalibleGameRepository;
        private readonly IGameRepository _gameRepository;

        public LibraryModel(IGameRepository gamesRepository, IAvalibleGameRepository avalibleGamesRepository)
        {
            _avalibleGameRepository = avalibleGamesRepository;
            _gameRepository = gamesRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Game> Games { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(StaticVariables.Login))
                return RedirectToPage("/ClientPages/Login");
            var avalibleGames = _avalibleGameRepository.GetGamesByUser(StaticVariables.Login).ToList();
            var gamesList = new List<Game>();
            foreach (var avalibleGame in avalibleGames)
                gamesList.Add(_gameRepository.GetGame(avalibleGame.GameName));
            Games = gamesList.AsEnumerable();
            if (!string.IsNullOrEmpty(SearchTerm))
                Search();
            return Page();
        }

        private void Search()
        {
            Games = Games.Where(g => g.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                                 || g.CreatedBy.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                                 || g.Genre.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                         .OrderBy(g => g.Name);
        }
    }
}
