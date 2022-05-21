using Steam3.Models;

namespace Steam3.Services.Interfaces
{
    public interface IAvalibleGameRepository
    {
        IEnumerable<AvalibleGame> GetGamesByUser(string clientLogin);
        IEnumerable<AvalibleGame> GetGamesByName(string gameName);
        AvalibleGame Add(AvalibleGame avalibleGame);

    }
}
