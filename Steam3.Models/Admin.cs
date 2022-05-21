using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam3.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        [MinLength(3), MaxLength(50)]
        public string Login { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
