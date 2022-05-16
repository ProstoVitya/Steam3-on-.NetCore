using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class EditModel : PageModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAvalibleGameRepository _avalibleGame;

        public EditModel(IClientRepository clientRepository, IAvalibleGameRepository avalibleGame)
        {
            _clientRepository = clientRepository;
            _avalibleGame = avalibleGame;
        }

        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.Login))
                Client = _clientRepository.GetClient(StaticVariables.Login);
            else
            {
                Client = new Client();
                Client.CreditCard1 = new CreditCard();
            }
            if (Client == null)
                return RedirectToPage("/NotFound");
            return Page();
        }

        public IActionResult OnPost()
        {
            Client.CreditCard1 = new CreditCard { Number = Client.CreditCard, Money = 9999 };
            if (ModelState.IsValid)
            {
                var savedLogin = Client.Login;
                if (!string.IsNullOrWhiteSpace(StaticVariables.Login))
                {
                    Client = _clientRepository.Update(StaticVariables.Login, Client);
                    if (Client != null)
                    {
                        foreach (var game in _avalibleGame.GetGamesByUser(StaticVariables.Login))
                        {
                            game.UserLogin = Client.Login;
                            TempData["SuccessMessage"] = $"{Client.Name} успешно обновлен!";
                            StaticVariables.Login = Client.Login;
                        }
                    }
                    else
                        TempData["SuccessMessage"] = $"{savedLogin} клиент с таким логином уже существует!";
                }
                else
                {
                    Client = _clientRepository.Add(Client);
                    TempData["SuccessMessage"] = Client == null ? $"{savedLogin} клиент с таким логином уже существует!" :
                                             $"{Client.Name} профиль успешно создан!";
                }
                if (Client != null)
                    StaticVariables.Login = Client.Login;
                return RedirectToPage("../Index");
            }
            return Page();
        }

        public IActionResult OnPostExit()
        {
            StaticVariables.Login = "";
            StaticVariables.IsAdmin = false;
            return RedirectToPage("/ClientPages/Login");
        }
    }
}
