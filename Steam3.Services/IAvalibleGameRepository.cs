using Steam3.Models;

namespace Steam3.Services
{
    public interface IAvalibleGameRepository
    {
        IEnumerable<AvalibleGame> GetGames(string clientLogin);
        AvalibleGame Add(AvalibleGame avalibleGame);
        
    }
}
