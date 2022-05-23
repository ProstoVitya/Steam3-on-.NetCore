using Steam3.Models;
using Steam3.Services.Interfaces;

namespace Steam3.Services.SqlRepositories
{
    public class SqlGameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public SqlGameRepository(AppDbContext context)
        {
            _context = context;
        }

        public Game Add(Game newGame)
        {
            var sameGame = _context.Games.Find(newGame.Name);
            if (sameGame == null)
            {
                _context.Games.Add(newGame);
                _context.SaveChanges();
                return newGame;
            }
            return null;
        }

        public Game Delete(string name)
        {
            var sameGame = _context.Games.Find(name);
            if (sameGame != null)
            {
                _context.Games.Remove(sameGame);
                _context.SaveChanges();
                return sameGame;
            }
            return sameGame;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games;
        }

        public Game GetGame(string name)
        {
            return _context.Games.Find(name);
        }

        public IEnumerable<Game> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _context.Games;
            var games = _context.Games.ToList();
            return games.Where(g => g.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                       || g.CreatedBy.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                       || g.Genre.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public Game Update(Game updatedGame)
        {
            var game = _context.Games.Find(updatedGame.Name);
            if (game != null)
            {
                game.CreatedBy = updatedGame.CreatedBy;
                game.Genre = updatedGame.Genre;
                game.Cost = updatedGame.Cost;
                game.PhotoPath = updatedGame.PhotoPath;
            }
            return game;
        }
    }
}
