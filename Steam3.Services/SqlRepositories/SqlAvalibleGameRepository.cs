using Steam3.Models;
using Steam3.Services.Interfaces;

namespace Steam3.Services.SqlRepositories
{
    public class SqlAvalibleGameRepository : IAvalibleGameRepository
    {
        private readonly AppDbContext _context;

        public SqlAvalibleGameRepository(AppDbContext context)
        {
            _context = context;
        }

        public AvalibleGame Add(AvalibleGame avalibleGame)
        {
            var existingGame = _context.AvalibleGames.Find(avalibleGame.Id);
            if (existingGame == null)
            {
                _context.AvalibleGames.Add(avalibleGame);
                _context.SaveChanges();
                return avalibleGame;
            }
            return null;
        }

        public IEnumerable<AvalibleGame> GetGamesByName(string gameName)
        {
            return _context.AvalibleGames.Where(g => g.GameName == gameName);
        }

        public IEnumerable<AvalibleGame> GetGamesByUser(string clientLogin)
        {
            return _context.AvalibleGames.Where(g => g.UserLogin == clientLogin);
        }
    }
}
