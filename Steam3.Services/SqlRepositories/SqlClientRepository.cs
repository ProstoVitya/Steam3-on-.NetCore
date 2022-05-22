using Steam3.Models;
using Steam3.Services.Interfaces;

namespace Steam3.Services.SqlRepositories
{
    public class SqlClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public SqlClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public Client Add(Client newClient)
        {
            var clientWithTheSameLogin = _context.Clients.Find(newClient.Login);
            if (clientWithTheSameLogin == null)
            {
                _context.Clients.Add(newClient);
                _context.SaveChanges();
                return newClient;
            }
            return null;
        }

        public Client Delete(string login)
        {
            var clientToDelete = _context.Clients.Find(login);
            if (clientToDelete != null)
            {
                foreach (var avalibleGame in clientToDelete.AvalibleGames)
                    _context.AvalibleGames.Remove(avalibleGame);
                _context.CreditCards.Remove(_context.CreditCards.Find(clientToDelete.CreditCard));
                _context.Clients.Remove(clientToDelete);
            }
            _context.SaveChanges();
            return clientToDelete;
        }

        public IEnumerable<Client> GetAllGames()
        {
            return _context.Clients;
        }

        public Client GetClient(string login)
        {
            return _context.Clients.Find(login);
        }

        public IEnumerable<Client> Search(string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return _context.Clients;
            return _context.Clients.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                        || c.Login.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public Client Update(string login, Client updatedClient)
        {
            var clientToUpdate = _context.Clients.Find(login);
            if (clientToUpdate != null)
            {
                foreach (var avalibleGame in _context.AvalibleGames.Where(g => g.UserLogin.Equals(login)))
                {
                    avalibleGame.UserLogin = updatedClient.Login;
                }
                clientToUpdate.Login = updatedClient.Login;
                clientToUpdate.Password = updatedClient.Password;
                clientToUpdate.Name = updatedClient.Name;
                clientToUpdate.CreditCard = updatedClient.CreditCard;
                _context.SaveChanges();
                return updatedClient;
            }
            return null;
        }
    }
}
