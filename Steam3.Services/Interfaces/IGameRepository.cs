using Steam3.Models;

namespace Steam3.Services.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> Search(string searchTerm);
        Game GetGame(string name);
        Game Update(Game updatedGame);
        Game Add(Game newGame);
        Game Delete(string name);
    }
}