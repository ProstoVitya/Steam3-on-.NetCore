using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;
using Steam3.Services.Interfaces;

namespace Steam3.RazorPages.Pages.GamePages
{
    public enum SortBy
    {
        Name,
        Count,
        Earnings
    }

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
        /*[BindProperty(SupportsGet = true)]
        public SortBy SortBy { get; set; }*/

        public IActionResult OnGet(SortBy sortBy)
        {
            if (!StaticVariables.IsAdmin)
            {
                TempData["SuccessMessage"] = "You should me logged as Admin";
                return RedirectToPage("../Index");
            }
            var games = _gameRepository.GetAllGames().ToList();
            foreach (var game in games)
            {
                int copies = _avalibleGameRepository.GetGamesByName(game.Name).Count();
                gamesStatistic.Add(game, new int[] { copies, copies * game.Cost });
            }
            SortGames(sortBy);
            return Page();
        }

        private void SortGames(SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.Name:
                    gamesStatistic = gamesStatistic.OrderBy(pair => pair.Key.Name)
                                                   .ToDictionary(pair => pair.Key, pair => pair.Value);
                    break;
                case SortBy.Count:
                    gamesStatistic = gamesStatistic.OrderByDescending(pair => pair.Value[0])
                                                   .ThenBy(pair => pair.Key.Name)
                                                   .ToDictionary(pair => pair.Key, pair => pair.Value);
                    break;
                case SortBy.Earnings:
                    gamesStatistic = gamesStatistic.OrderByDescending(pair => pair.Value[1])
                                                   .ThenBy(pair => pair.Key.Name)
                                                   .ToDictionary(pair => pair.Key, pair => pair.Value);
                    break;
                default:
                    throw new ArgumentException("Incorrect state SortBy");
                    break;
            }
        }
    }
}
