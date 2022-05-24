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

        public Client GetClient(string login)
        {
            return _context.Clients.Find(login);
        }

        public Client Update(string login, Client updatedClient)
        {
            var clientToUpdate = _context.Clients.Find(login);
            if (clientToUpdate != null &&
                _context.CreditCards.Find(updatedClient.CreditCard) == null)
            {
                clientToUpdate.Password = updatedClient.Password;
                clientToUpdate.Name = updatedClient.Name;

                _context.CreditCards.Remove(_context.CreditCards.Find(clientToUpdate.CreditCard));
                clientToUpdate.CreditCard = updatedClient.CreditCard;
                var creditCard = new CreditCard{Number = clientToUpdate.CreditCard,Money = 9999};
                _context.CreditCards.Add(creditCard);
                clientToUpdate.CreditCard1 = creditCard;
                _context.Clients.Update(clientToUpdate);
                _context.SaveChanges();
                return updatedClient;
            }
            return null;
        }
    }
}
