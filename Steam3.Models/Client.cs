using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("Clients")]
    public partial class Client
    {
        public Client()
        {
            AvalibleGames = new HashSet<AvalibleGame>();
        }

        [Key]
        [MinLength(3), MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [Required, ForeignKey("CreditCard1")]
        public int CreditCard { get; set; }

        public virtual ICollection<AvalibleGame> AvalibleGames { get; set; }

        public virtual CreditCard? CreditCard1 { get; set; }
    }
}
