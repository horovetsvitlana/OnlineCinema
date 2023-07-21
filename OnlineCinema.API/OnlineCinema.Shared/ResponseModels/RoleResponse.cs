namespace OnlineCinema.Shared.ResponseModels
{
    public class RoleResponse
    {
        public string RoleName { get; set; }
        public ICollection<UserResponse> Users { get; set; }
    }
}
