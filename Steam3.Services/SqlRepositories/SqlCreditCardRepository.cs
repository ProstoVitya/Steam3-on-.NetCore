using Steam3.Models;
using Steam3.Services.Interfaces;

namespace Steam3.Services.SqlRepositories
{
    public class SqlCreditCardRepository : ICreditCardRepository
    {
        private readonly AppDbContext _context;

        public SqlCreditCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public CreditCard Add(CreditCard creditCard)
        {
            var sameCard = _context.CreditCards.Find(creditCard.Number);
            if (sameCard == null)
            {
                _context.CreditCards.Add(creditCard);
                _context.SaveChanges();
                return creditCard;
            }
            return null;
        }

        public CreditCard Delete(CreditCard creditCard)
        {
            var cardToDelete = _context.CreditCards.Find(creditCard.Number);
            if (cardToDelete != null)
            {
                _context.CreditCards.Remove(cardToDelete);
                _context.SaveChanges();
                return cardToDelete;
            }
            return null;
        }

        public CreditCard GetCard(int number)
        {
            return _context.CreditCards.Find(number);
        }
    }
}
