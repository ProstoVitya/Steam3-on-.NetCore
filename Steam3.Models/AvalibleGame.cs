using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("AvalibleGame")]
    public partial class AvalibleGame
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Client")]
        [StringLength(32)]
        public string UserLogin { get; set; }

        [ForeignKey("Game")]
        [StringLength(32)]
        public string GameName { get; set; }

        public virtual Client Client { get; set; }

        public virtual Game Game { get; set; }
    }
}