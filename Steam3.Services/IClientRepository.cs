using Steam3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam3.Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllGames();
        IEnumerable<Client> Search(string searchTerm);
        Client GetClient(string login);
        Client Update(Client updatedClient);
        Client Add(Client newClient);
        Client Delete(string login);
    }
}
