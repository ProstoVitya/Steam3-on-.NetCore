using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class LoginModel : PageModel
    {
        private readonly IClientRepository _clientRepository;

        public LoginModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            if(!string.IsNullOrWhiteSpace(StaticVariables.Login))
            {
                return RedirectToPage("/ClientPages/Edit");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var client = _clientRepository.GetClient(Login);
                if (client != null && client.Password == Password)
                {
                    StaticVariables.Login = client.Login;
                    return RedirectToPage("../Index");
                }
            }
                return Page();
        }
    }
}
