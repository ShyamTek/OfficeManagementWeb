using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tawuniya.Core.Domain.Users;
using Tawuniya.Data;
using Tawuniya.Services.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Tawuniya.Services.Users
{
    public partial class UserService : IUserService
    {
        #region Fields

        private readonly TawuniyaDBContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public UserService(TawuniyaDBContext context,
            IEncryptionService encryptionService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _encryptionService = encryptionService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Utilities

        protected async Task<bool> PasswordMatch(User user, string enterdPassword)
        {
            if (user == null || string.IsNullOrEmpty(enterdPassword))
                return false;

            var userPassword = await GetUserPasswordByUserIdAsync(user.Id);

            string? savedPassword;
            if (!string.IsNullOrEmpty(userPassword.PasswordSalt))
                savedPassword = _encryptionService.CreatePasswordHash(enterdPassword, userPassword.PasswordSalt, "SHA512");
            else
                savedPassword = enterdPassword;

            if (userPassword == null)
                return false;

            return userPassword.Password.Equals(savedPassword);
        }

        protected async Task SignIn(User? user, bool rememberMe)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, "Tawuniya"));

            var userIdentity = new ClaimsIdentity(claims, "Authentication");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                IssuedUtc = DateTime.UtcNow
            };

            await _httpContextAccessor.HttpContext.SignInAsync("Authentication", userPrincipal, authenticationProperties);

        }

        #endregion

        #region Methods

        /// <summary>
        /// get all users 
        /// </summary>
        /// <returns>list of users</returns>
        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// get user by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// get user by email async
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        /// <summary>
        /// insert user async
        /// </summary>
        /// <param name="user">user</param>
        public async Task InsertUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update user async
        /// </summary>
        /// <param name="user">user</param>
        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete user async
        /// </summary>
        /// <param name="user">user</param>
        public async Task DeleteUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// insert user password
        /// </summary>
        /// <param name="userPassword"></param>
        public async Task InsertUserPassword(UserPassword userPassword)
        {
            await _context.UserPassword.AddAsync(userPassword);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update user password
        /// </summary>
        /// <param name="userPassword"></param>
        public async Task UpdateUserPassword(UserPassword userPassword)
        {
            _context.UserPassword.Update(userPassword);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete user password
        /// </summary>
        /// <param name="userPassword"></param>
        public async Task DeleteUserPassword(UserPassword userPassword)
        {
            _context.UserPassword.Remove(userPassword);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get user password by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserPassword> GetUserPasswordByUserIdAsync(int userId)
        {
            return await _context.UserPassword.Where(up => up.UserId == userId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// validate user
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>login result</returns>
        public async Task<string> ValidateUserAsync(string email, string password)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44377/Api/User/Login?email={email}&password={password}");
            request.Headers.Add("accept", "*/*");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            var loginResponse = await response.Content.ReadAsStringAsync();

            return loginResponse;
        }

        /// <summary>
        /// sign in user
        /// </summary>
        /// <param name="user">user </param>
        /// <param name="rememberMe">remember me</param>
        /// <returns>sign in user</returns>
        public virtual async Task<bool> SignInUserAsync(User? user, bool rememberMe)
        {
            await SignIn(user, rememberMe);

            return true;
        }

        /// <summary>
        /// sign out user
        /// </summary>
        public virtual async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("Authentication");
        }

        /// <summary>
        /// api post method
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsonBody">json body</param>
        /// <returns></returns>
        public async Task<string> ApiPostAsync(string url, string jsonBody)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "*/*");
            var content = new StringContent(jsonBody, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var registrationResponse = await response.Content.ReadAsStringAsync();

            return registrationResponse;
        }

        #endregion
    }
}
