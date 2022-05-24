using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;
using Steam3.Services.Interfaces;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class EditModel : PageModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAvalibleGameRepository _avalibleGame;
        private readonly ICreditCardRepository _creditCardRepository;

        public EditModel(IClientRepository clientRepository, IAvalibleGameRepository avalibleGame,
            ICreditCardRepository creditCardRepository)
        {
            _clientRepository = clientRepository;
            _avalibleGame = avalibleGame;
            _creditCardRepository = creditCardRepository;
        }

        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet()
        {
            if (StaticVariables.IsAdmin)
                return Page();
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
            if(!string.IsNullOrWhiteSpace(StaticVariables.Login))
                Client.Login = StaticVariables.Login;
            ModelState.ClearValidationState(nameof(Client));
            if (TryValidateModel(Client, nameof(Client)))
            {
                AddCard();
                if (string.IsNullOrWhiteSpace(StaticVariables.Login))
                    AddClient();
                else
                    UpdateClient();

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

        public IActionResult OnPostDelete()
        {
            _clientRepository.Delete(StaticVariables.Login);
            StaticVariables.Login = "";
            return RedirectToPage("../Index");
        }

        private void AddCard()
        {
            var card = new CreditCard { Number = Client.CreditCard, Money = 9999 };
            Client.CreditCard1 = Client.CreditCard1 = card;
        }

        private void UpdateClient()
        {
            Client = _clientRepository.Update(StaticVariables.Login, Client);
            if (Client != null)
            {
                TempData["SuccessMessage"] = $"{Client.Name} успешно обновлен!";
                StaticVariables.Login = Client.Login;
            }
            else
                TempData["SuccessMessage"] = $"Ќеверные данные!";
        }

        private void AddClient()
        {
            var savedLogin = Client.Login;
            Client = _clientRepository.Add(Client);
            TempData["SuccessMessage"] = Client == null ? $"{savedLogin} клиент с таким логином уже существует!" :
                                     $"{Client.Name} профиль успешно создан!";
        }
    }
}
