using Steam3.Models;

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
            if (clientWithTheSameLogin == null)
                _clientList.Add(newClient);
            else
                newClient = null;
            return newClient;
        }

        //Add cascade deleting
        public Client Delete(string login)
        {
            var clientToDelete = _clientList.Find(c => c.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
            if (clientToDelete != null)
            {
                foreach (var avalibleGame in clientToDelete.AvalibleGames)
                {
                    
                }
                _clientList.Remove(clientToDelete);
            }
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

        //add cascade update
        public Client Update(string login, Client updatedClient)
        {
            var clientWithTheSameLogin = _clientList.Find(c => c.Login.Equals(updatedClient.Login));
            if (clientWithTheSameLogin != null)
                return null;

            var clientToUpdate = _clientList.Find(c => c.Login.Equals(login));
            if (clientToUpdate != null)
            {
               /* IAvalibleGameRepository avalible = new MockAvalibleGameRepository();
                foreach (var avalibleGame in avalible.GetGames(clientToUpdate.Login))
                {
                    avalibleGame.UserLogin = updatedClient.Login;
                }*/
                clientToUpdate.Login = updatedClient.Login;
                clientToUpdate.Password = updatedClient.Password;
                clientToUpdate.CreditCard=updatedClient.CreditCard;
                clientToUpdate.Name=updatedClient.Name;
            }
            return clientToUpdate;
        }
    }
}
