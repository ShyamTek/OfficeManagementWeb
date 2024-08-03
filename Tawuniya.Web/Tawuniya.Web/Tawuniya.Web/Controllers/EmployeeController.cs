using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tawuniya.Core.Domain.Employees;
using Tawuniya.Services.Common;
using Tawuniya.Services.Employees;
using Tawuniya.Web.Factories;
using Tawuniya.Web.Models.Employees;

namespace Tawuniya.Web.Controllers
{
    public class EmployeeController : Controller
    {
        #region Fields

        private readonly IEmployeeModelFactory _employeeModelFactory;
        private readonly ICommonAPIService _commonAPIService;

        #endregion

        #region Ctor

        public EmployeeController(IEmployeeModelFactory employeeModelFactory,
            ICommonAPIService commonAPIService)
        {
            _employeeModelFactory = employeeModelFactory;
            _commonAPIService = commonAPIService;
        }

        #endregion

        #region Methods

        public IActionResult List()
        {
            ViewBag.Current = "Employee";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeList()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44377/api/Employee/EmployeeList");
            request.Headers.Add("accept", "*/*");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var employees = JsonConvert.DeserializeObject<IList<EmployeeListModel>>(await response.Content.ReadAsStringAsync());


            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            //var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int recordsTotal = employees.Count();
            var data = employees.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public IActionResult Create()
        {
            ViewBag.Current = "Employee";
            var model = _employeeModelFactory.PrepareEmployeeModel(new EmployeeModel(), null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (ModelState.IsValid) 
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                var response = await _commonAPIService.ApiPostAsync("https://localhost:44377/api/Employee/CreateEmployee", jsonBody);

            }
            model = _employeeModelFactory.PrepareEmployeeModel(model, null);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Current = "Employee";

            var employeeResponse = await _commonAPIService.GetByIdAsync("https://localhost:44377/api/Employee/GetEmployee", id);
            var employee = (Employee)employeeResponse;
            if (employee == null)
                return RedirectToAction("List");
            var userModel = _employeeModelFactory.PrepareEmployeeModel(null, employee);

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            var employeeResponse = await _commonAPIService.GetByIdAsync("https://localhost:44377/api/Employee/GetEmployee", model.Id);
            var employee = (Employee)employeeResponse;
            if (employee == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                var response = await _commonAPIService.ApiPostAsync("https://localhost:44377/api/Employee/UpdateEmployee", jsonBody);

                return RedirectToAction("List");
            }
            model = _employeeModelFactory.PrepareEmployeeModel(model, employee);
            return View(model);
        }

        #endregion
    }
}
