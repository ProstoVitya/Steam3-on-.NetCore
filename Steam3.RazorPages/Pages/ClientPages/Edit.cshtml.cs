using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class EditModel : PageModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IClientRepository clientRepository, IWebHostEnvironment webHostEnvironment)
        {
            _clientRepository = clientRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Client Client { get; set; }
        public IActionResult OnGet(string login)
        {
            if(!string.IsNullOrWhiteSpace(login))
                Client = _clientRepository.GetClient(login);
            else
                Client = new Client();
            if (Client == null)
                return RedirectToPage("/NotFound");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Client.Login != null)
                {
                    Client = _clientRepository.Update(Client);
                    TempData["SuccessMessage"] = $"{Client.Name} успешно обновлен!";
                }
                else
                {
                    Client = _clientRepository.Add(Client);
                    TempData["SuccessMessage"] = $"{Client.Name} профиль успешно создан!";
                }
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
