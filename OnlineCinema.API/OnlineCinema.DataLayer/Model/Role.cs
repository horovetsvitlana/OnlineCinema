using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCinema.DataLayer.Model
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
