using Steam3.Models;

namespace Steam3.Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllGames();
        IEnumerable<Client> Search(string searchTerm);
        Client GetClient(string login);
        Client Update(string login, Client updatedClient);
        Client Add(Client newClient);
        Client Delete(string login);
    }
}
