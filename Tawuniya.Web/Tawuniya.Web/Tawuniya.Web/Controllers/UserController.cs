using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tawuniya.Services.Common;
using Tawuniya.Services.Users;
using Tawuniya.Web.Factories;
using Tawuniya.Web.Models.Users;

namespace Tawuniya.Web.Controllers
{
    public class UsersController : Controller
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly ICommonAPIService _commonAPIService;
        private readonly IUserModelFactory _userModelFactory;

        #endregion

        #region Ctor

        public UsersController(IUserService userService,
            ICommonAPIService commonAPIService,
            IUserModelFactory userModelFactory)
        {
            _userService = userService;
            _commonAPIService = commonAPIService;
            _userModelFactory = userModelFactory;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.Current = "User";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserList()
        {
            var response = await _commonAPIService.EntityListAsync("https://localhost:44377/api/User/GetAllUsers");
            var users = JsonConvert.DeserializeObject<IList<UserCreationResponse>>(response);


            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            //var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = model.UserName;

                var loginResult = await _userService.ValidateUserAsync(userId, model.Password);
                if (!string.IsNullOrEmpty(loginResult) && loginResult.Contains("successfull"))
                {
                    var user = await _userService.GetUserByEmailAsync(userId);
                    await _userService.SignInUserAsync(user, model.Rememberme);

                    return RedirectToAction("Index", "Home", null);
                }
                else
                    ModelState.AddModelError("", loginResult);
            }
            model = await _userModelFactory.PrepareLoginModel();
            return View(model);
        }

        public IActionResult RegisterUser()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var jsonBody = JsonConvert.SerializeObject(registerUser);
                var registration = await _commonAPIService.ApiPostAsync("https://localhost:44377/Api/User/CreateUser", jsonBody);
                if(!string.IsNullOrEmpty(registration) && registration.Contains("success"))
                    return RedirectToAction("Index", "Home", null);

            }

            return View(registerUser);
        }
    }
}