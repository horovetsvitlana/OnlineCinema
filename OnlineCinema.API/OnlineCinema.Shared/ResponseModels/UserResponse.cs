using OnlineCinema.DataLayer.Model;

namespace OnlineCinema.Shared.ResponseModels
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }


    }
}
