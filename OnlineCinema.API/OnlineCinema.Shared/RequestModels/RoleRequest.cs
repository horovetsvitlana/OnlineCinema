namespace OnlineCinema.Shared.RequestModels
{
    public class RoleRequest
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection< UserRequest> Users { get; set; }
    }
}
