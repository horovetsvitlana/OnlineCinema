namespace OnlineCinema.Shared.RequestModels
{
    public class UserRequest
    {
        public string Login { get; set; }
        public int DateOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }
    }
}
