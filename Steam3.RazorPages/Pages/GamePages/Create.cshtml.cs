using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steam3.Models;
using Steam3.Services;

namespace Steam3.RazorPages.Pages.GamePages
{
    public class CreateModel : PageModel
    {
        private readonly IGameRepository _gameRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IGameRepository gameRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _gameRepository = gameRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Game Game { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        public IActionResult OnGet()
        {
            if (!StaticVariables.IsAdmin)
            {
                TempData["SuccessMessage"] = "You should me logged as Admin";
                return RedirectToPage("../Index");
            }
            Game = new Game();
            if (Game == null)
                return NotFound();
            return Page();
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Game.PhotoPath != null && !Game.PhotoPath.Equals("noimage.png"))
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Game.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Game.PhotoPath = ProcessUploadedFile();
                }
                string saveGameName = Game.Name;
                Game = _gameRepository.Add(Game);
                if (Game != null)
                {
                    TempData["SuccessMessage"] = saveGameName + " успешно добавлена!";
                    return RedirectToPage("../Index");
                }
                else
                {
                    Game = new Game();
                    TempData["SuccessMessage"] = saveGameName + " уже есть в магазине!";
                    return Page();
                }
            }
            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }                
            }
            return uniqueFileName;
        }
    }
}
