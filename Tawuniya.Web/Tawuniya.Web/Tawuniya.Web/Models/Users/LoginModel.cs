namespace Tawuniya.Web.Models.Users
{
    public class LoginModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}
