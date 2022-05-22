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
            //_gamesRepository.Dispose();
        }


        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(StaticVariables.Login))
                return RedirectToPage("/ClientPages/Login");
            var avalibleGames = _avalibleGameRepository.GetGamesByUser(StaticVariables.Login).ToList();
            var gamesList = new List<Game>();
            foreach (var avalibleGame in avalibleGames)
                gamesList.Add(_gameRepository.GetGame(avalibleGame.GameName));
            Games = gamesList.AsEnumerable();
            return Page();
        }

        public IEnumerable<Game> Games { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
