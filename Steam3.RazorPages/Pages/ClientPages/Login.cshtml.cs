using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.ClientPages
{
    public class LoginModel : PageModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;

        public LoginModel(IClientRepository clientRepository, IAdminRepository adminRepository)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
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
                var admin = _adminRepository.GetAdmin(Login);
                if (admin != null && admin.Password == Password)
                {
                    StaticVariables.Login = admin.Login;
                    StaticVariables.IsAdmin = true;
                    return RedirectToPage("../Index");
                }

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
