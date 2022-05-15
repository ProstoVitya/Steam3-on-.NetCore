using Steam3.Models;

namespace Steam3.Services
{
    public interface IAvalibleGameRepository
    {
        IEnumerable<AvalibleGame> GetGamesByUser(string clientLogin);
        IEnumerable<AvalibleGame> GetGamesByName(string gameName);
        AvalibleGame Add(AvalibleGame avalibleGame);
        
    }
}
