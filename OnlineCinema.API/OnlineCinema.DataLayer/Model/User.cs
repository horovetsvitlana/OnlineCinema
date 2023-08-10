using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineCinema.DataLayer.Model
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MinLength(5)]
        [MaxLength(20)]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public int DateOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}
