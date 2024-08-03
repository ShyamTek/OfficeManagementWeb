using Tawuniya.Core.Domain.Users;

namespace Tawuniya.Services.Users
{
    public partial interface IUserService
    {
        /// <summary>
        /// get all users 
        /// </summary>
        /// <returns>list of users</returns>
        Task<IList<User>> GetAllUsersAsync();

        /// <summary>
        /// get user by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// get user by email async
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// insert user async
        /// </summary>
        /// <param name="user">user</param>
        Task InsertUserAsync(User user);

        /// <summary>
        /// update user async
        /// </summary>
        /// <param name="user">user</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// delete user async
        /// </summary>
        /// <param name="user">user</param>
        Task DeleteUserAsync(User user);

        /// <summary>
        /// insert user password
        /// </summary>
        /// <param name="userPassword"></param>
        Task InsertUserPassword(UserPassword userPassword);

        /// <summary>
        /// update user password
        /// </summary>
        /// <param name="userPassword"></param>
        Task UpdateUserPassword(UserPassword userPassword);

        /// <summary>
        /// delete user password
        /// </summary>
        /// <param name="userPassword"></param>
        Task DeleteUserPassword(UserPassword userPassword);

        /// <summary>
        /// get user password by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserPassword> GetUserPasswordByUserIdAsync(int userId);

        /// <summary>
        /// validate user
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>login result</returns>
        Task<string> ValidateUserAsync(string email, string password);

        /// <summary>
        /// sign in user
        /// </summary>
        /// <param name="user">user </param>
        /// <param name="rememberMe">remember me</param>
        /// <returns>sign in user</returns>
        Task<bool> SignInUserAsync(User? user, bool rememberMe);

        /// <summary>
        /// sign out user
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// api post method
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsonBody">json body</param>
        /// <returns></returns>
        Task<string> ApiPostAsync(string url, string jsonBody);
    }
}
