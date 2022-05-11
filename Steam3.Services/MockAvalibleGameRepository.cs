using Steam3.Models;

namespace Steam3.Services
{
    public class MockAvalibleGameRepository : IAvalibleGameRepository
    {
        private List<AvalibleGame> _avalibleGames;

        public MockAvalibleGameRepository()
        {
            _avalibleGames = new List<AvalibleGame>()
            {
                new AvalibleGame { UserLogin = "Login1", GameName = "TestGame" },
                new AvalibleGame { UserLogin = "Login1", GameName = "Grido Islando" },
                new AvalibleGame { UserLogin = "Login1", GameName = "Elden Ring" }
            };
        }

        public AvalibleGame Add(AvalibleGame avalibleGame)
        {
            var existingGame = _avalibleGames.Find(g => g.UserLogin == avalibleGame.UserLogin
                                                      && g.GameName == avalibleGame.GameName);
            if (existingGame == null)
            {
                _avalibleGames.Add(avalibleGame);
                return avalibleGame;
            }
            return null;
        }

        public IEnumerable<AvalibleGame> GetGames(string clientLogin)
        {
            return _avalibleGames.Where(g => g.UserLogin == clientLogin);
        }
    }
}
