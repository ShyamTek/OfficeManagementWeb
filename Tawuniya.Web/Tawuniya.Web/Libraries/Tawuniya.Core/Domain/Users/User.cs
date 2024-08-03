namespace Tawuniya.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
