using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("AvalibleGames")]
    public partial class AvalibleGame
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        [StringLength(50)]
        public string UserLogin { get; set; }

        [ForeignKey("Game")]
        [StringLength(50)]
        public string GameName { get; set; }

        public virtual Client Client { get; set; }

        public virtual Game Game { get; set; }
    }
}