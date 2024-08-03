using Tawuniya.Services.Users;
using Tawuniya.Web.Models.Users;

namespace Tawuniya.Web.Factories
{
     public class UserModelFactory : IUserModelFactory
     {
          #region Fields

          private readonly IUserService _userService;

          #endregion

          #region Ctor

          public UserModelFactory(IUserService userService)
          {
               _userService = userService;
          }

          #endregion

          #region Methods

          /// <summary>
          /// prepare Login Model
          /// </summary>
          /// <returns>Login Model</returns>
          public Task<LoginModel> PrepareLoginModel()
          {
               var model = new LoginModel();

               return Task.FromResult(model);
          }

          

          #endregion
     }
}
