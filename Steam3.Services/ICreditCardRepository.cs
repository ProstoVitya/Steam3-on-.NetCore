using Steam3.Models;

namespace Steam3.Services
{
    public interface ICreditCardRepository
    {
        public CreditCard Add(CreditCard creditCard);
        public CreditCard Delete(CreditCard creditCard);
        public CreditCard GetCard(int number);
    }
}
