using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Steam3.Models
{
    [Table("Game")]
    public partial class Game
    {
        [Key]
        [MinLength(5), MaxLength(50)]
        [Required(ErrorMessage = "Name must be not null and uniqe")]
        public string Name { get; set; }
        
        [Required]
        public int Cost { get; set; }

        [Required, MaxLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public string? PhotoPath { get; set; }

        public virtual ICollection<AvalibleGame>? AvalibleGames { get; set; }
    }
}
