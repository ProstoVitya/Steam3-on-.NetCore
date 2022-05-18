using Microsoft.AspNetCore.Mvc;

namespace Steam3.RazorPages.Controllers
{
    public class GameViewController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToPage("NotFounded");
        }
    }
}
