using Steam3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam3.Services
{
    public class MockGameRepository : IGameRepository
    {
        private List<Game> _gameList;

        public MockGameRepository()
        {
            _gameList = new List<Game>()
            {
                new Game { Name = "TestGame", Cost = 1337, Genre = Genre.Arcade, CreatedBy = "TestUser"},
                new Game { Name = "Grido Islando", Cost = 9999, Genre = Genre.BattleRoyale, CreatedBy = "Ginn Frix"},
                new Game { Name = "Elden Ring", Cost = 1999, Genre = Genre.Simulator, CreatedBy = "FromSofrware"},
                new Game { Name = "Some Game", Cost = 1, Genre = Genre.None, CreatedBy = "Some User"},
                new Game { Name = "Stop", Cost = 10, Genre = Genre.SportsGame, CreatedBy = "Lol"}
            };
        }

        public Game Add(Game newGame)
        {
            var gameWithTheSameName = _gameList.Find(g => g.Name == newGame.Name);
            if (gameWithTheSameName == null)
            {
                _gameList.Add(newGame);
            }
            else
                newGame = null;
            return newGame;
        }

        public Game Delete(string name)
        {
            var gameToDelete = _gameList.Find(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if(gameToDelete != null)
                _gameList.Remove(gameToDelete);
            return gameToDelete;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _gameList;
        }

        public Game GetGame(string name)
        {
            return _gameList.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Game> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _gameList;
            return _gameList.Where(g => g.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                     || g.CreatedBy.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                     || g.Genre.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Game> SearchByUser(IEnumerable<AvalibleGame> avalibleGames)
        {
            var gamesList = new List<Game>();
            foreach (var avalibleGame in avalibleGames)
                foreach (var game in _gameList)
                    if(avalibleGame.GameName == game.Name)
                        gamesList.Add(game);
            return gamesList.AsEnumerable();
                
        }

        public Game Update(Game updatedGame)
        {
            var game = _gameList.Find(g => g.Name.Equals(updatedGame.Name, StringComparison.OrdinalIgnoreCase));
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
