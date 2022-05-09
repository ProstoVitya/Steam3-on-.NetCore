using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("CreditCard")]
    public partial class CreditCard
    {
        public CreditCard()
        {
            Client = new HashSet<Client>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        public int Money { get; set; }

        public virtual ICollection<Client> Client { get; set; }
    }
}