using Steam3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam3.Services
{
    public class MockClientRepository : IClientRepository
    {
        private List<Client> _clientList;

        public MockClientRepository()
        {
            _clientList = new List<Client>()
            {
                new Client{ Login = "Login1", Password = "Password1", Name = "Name1", CreditCard = 112 },
                new Client{ Login = "Login2", Password = "Password2", Name = "Name2", CreditCard = 212 },
                new Client{ Login = "Login3", Password = "Password3", Name = "Name3", CreditCard = 312 },
                new Client{ Login = "Login4", Password = "Password4", Name = "Name4", CreditCard = 412 },
                new Client{ Login = "Login5", Password = "Password5", Name = "Name5", CreditCard = 512 },
            };
        }

        public Client Add(Client newClient)
        {
            var clientWithTheSameLogin = _clientList.Find(c => c.Login.Equals(newClient.Login));
            if (clientWithTheSameLogin != null)
                _clientList.Add(newClient);
            else
                newClient = null;
            return newClient;
        }

        public Client Delete(string login)
        {
            var clientToDelete = _clientList.Find(c => c.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
            if (clientToDelete != null)
                _clientList.Remove(clientToDelete);
            return clientToDelete;
        }

        public IEnumerable<Client> GetAllGames()
        {
            return _clientList;
        }

        public Client GetClient(string login)
        {
            return _clientList.Find(c => c.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Client> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _clientList;
            return _clientList.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                 || c.Login.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public Client Update(Client updatedClient)
        {
            var client = _clientList.Find(c => c.Login.Equals(updatedClient.Login, StringComparison.OrdinalIgnoreCase));
            if (client != null)
            {
                client.CreditCard=updatedClient.CreditCard;
                client.Password=updatedClient.Password;
                client.Name=updatedClient.Name;
            }
            return client;
        }
    }
}
