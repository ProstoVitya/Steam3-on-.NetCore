using Steam3.Models;

namespace Steam3.Services.Interfaces
{
    public interface IClientRepository
    {
        Client GetClient(string login);
        Client Update(string login, Client updatedClient);
        Client Add(Client newClient);
        Client Delete(string login);
    }
}
