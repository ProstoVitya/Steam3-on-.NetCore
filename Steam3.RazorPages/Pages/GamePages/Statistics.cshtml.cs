using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;
using Steam3.Services.Interfaces;

namespace Steam3.RazorPages.Pages.GamePages
{
    public class StatisticsModel : PageModel
    {
        private readonly IGameRepository _gameRepository;
        private readonly IAvalibleGameRepository _avalibleGameRepository;

        public StatisticsModel(IGameRepository gameRepository, IAvalibleGameRepository avalibleGameRepository)
        {
            _gameRepository = gameRepository;
            _avalibleGameRepository = avalibleGameRepository;
            gamesStatistic = new Dictionary<Game, int[]>();
        }

        [BindProperty]
        public Dictionary<Game, int[]> gamesStatistic { get; set; }

        public IActionResult OnGet()
        {
            if (!StaticVariables.IsAdmin)
            {
                TempData["SuccessMessage"] = "You should me logged as Admin";
                return RedirectToPage("../Index");
            }
            foreach (var game in _gameRepository.GetAllGames())
            {
                int copies = _avalibleGameRepository.GetGamesByName(game.Name).Count();
                gamesStatistic.Add(game, new int[] { copies, copies * game.Cost });
            }
            return Page();
        }
    }
}
