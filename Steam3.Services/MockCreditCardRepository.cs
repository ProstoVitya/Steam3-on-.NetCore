using Steam3.Models;

namespace Steam3.Services
{
    public class MockCreditCardRepository : ICreditCardRepository
    {
        private readonly List<CreditCard> _cards;

        public MockCreditCardRepository()
        {
            _cards = new List<CreditCard>()
            {
                new CreditCard{ Number = 112, Money = 9999 },
                new CreditCard{ Number = 212, Money = 9999 },
                new CreditCard{ Number = 312, Money = 9999 },
                new CreditCard{ Number = 412, Money = 9999 },
                new CreditCard{ Number = 512, Money = 9999 },
            };
        }

        public CreditCard Add(CreditCard creditCard)
        {
            var sameCard = _cards.FirstOrDefault(x => x.Number == creditCard.Number);
            if (sameCard == null)
            {
                _cards.Add(creditCard);
                return creditCard;
            }
            return null;
        }

        public CreditCard Delete(CreditCard creditCard)
        {
            var card = _cards.FirstOrDefault(c => c.Number == creditCard.Number);
            if (card != null)
            {
                _cards.Remove(card);
                return creditCard;
            }
            return null;
        }

        public CreditCard GetCard(int number)
        {
            return _cards.FirstOrDefault(c => c.Number == number);
        }
    }
}
