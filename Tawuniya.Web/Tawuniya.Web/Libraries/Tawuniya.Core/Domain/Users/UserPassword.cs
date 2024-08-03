namespace Tawuniya.Core.Domain.Users
{
    public class UserPassword : BaseEntity
    {
        public int UserId {  get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
