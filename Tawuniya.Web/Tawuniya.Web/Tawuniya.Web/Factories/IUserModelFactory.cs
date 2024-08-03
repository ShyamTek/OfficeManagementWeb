using Tawuniya.Web.Models.Users;

namespace Tawuniya.Web.Factories
{
    public interface IUserModelFactory
    {
        /// <summary>
        /// prepare Login Model
        /// </summary>
        /// <returns>Login Model</returns>
        Task<LoginModel> PrepareLoginModel();
    }
}
