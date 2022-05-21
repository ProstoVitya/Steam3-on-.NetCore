using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;
using Steam3.Services.Interfaces;

namespace Steam3.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly IAvalibleGameRepository _avalibleGameRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IClientRepository _clientRepository;

        public IndexModel(ILogger<IndexModel> logger, IGameRepository gameRepository,
            IAvalibleGameRepository avalibleGameRepository, ICreditCardRepository creditCardRepository,
            IClientRepository clientRepository)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _avalibleGameRepository = avalibleGameRepository;
            _creditCardRepository = creditCardRepository;
            _clientRepository = clientRepository;
        }

        [BindProperty]
        public IEnumerable<Game> Games { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            Games = _gameRepository.Search(SearchTerm);
        }

        public IActionResult OnPostBuy(string gameName, int cost)
        {
            if (StaticVariables.IsAdmin)
            {
                TempData["SuccessMessage"] = "Зайдите за обычного пользователя";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(StaticVariables.Login))
                return RedirectToPage("/ClientPages/Login");
           if(!HasGame(gameName))
                return Buy(gameName, cost);
            TempData["SuccessMessage"] = $"У вас уже есть {gameName}";
            return RedirectToPage("Index");
        }

        private bool HasGame(string gameName)
        {
            var clientAvalibleGames = _avalibleGameRepository.GetGamesByUser(StaticVariables.Login);
            foreach (var game in clientAvalibleGames)
                if (game.GameName == gameName)
                    return true;

            return false;
        }

        private IActionResult Buy(string gameName, int cost)
        {
            var client = _clientRepository.GetClient(StaticVariables.Login);
            var credtCard = _creditCardRepository.GetCard(client.CreditCard);
            if (credtCard.Money < cost)
            {
                TempData["SuccessMessage"] = "У вас недостаточно денег";
                return RedirectToPage("Index");

            }
            credtCard.Money -= cost;
            _avalibleGameRepository.Add(
                new AvalibleGame
                {
                    GameName = gameName,
                    UserLogin = StaticVariables.Login
                });
            TempData["SuccessMessage"] = $"{gameName} была успешно куплена и добавлена в библиотеку";
            return RedirectToPage("Index");

        }
    }
}