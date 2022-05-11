using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("AvalibleGames")]
    public partial class AvalibleGames
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserLogin { get; set; }

        [Required]
        [StringLength(32)]
        public string GameName { get; set; }

        public virtual Client Client { get; set; }

        public virtual Game Game { get; set; }
    }
}