using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tawuniya.Services.Common;
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
            var response = await _commonAPIService.EntityListAsync("https://localhost:44377/api/Employee/EmployeeList");
            var employees = JsonConvert.DeserializeObject<IList<EmployeeListModel>>(response);


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
            //var model = _employeeModelFactory.PrepareEmployeeModel(new EmployeeModel(), null);

            return View(new EmployeeModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                var response = await _commonAPIService.ApiPostAsync("https://localhost:44377/api/Employee/CreateEmployee", jsonBody);

            }
            //model = _employeeModelFactory.PrepareEmployeeModel(model, null);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Current = "Employee";

            var employeeResponse = await _commonAPIService.GetByIdAsync("https://localhost:44377/api/Employee/GetEmployee", id);
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(employeeResponse);
            if (employee == null)
                return RedirectToAction("List");
            //var userModel = _employeeModelFactory.PrepareEmployeeModel(null, employee);

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            var employeeResponse = await _commonAPIService.GetByIdAsync("https://localhost:44377/api/Employee/GetEmployee", model.Id);
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(employeeResponse);
            if (employee == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                var response = await _commonAPIService.ApiPostAsync("https://localhost:44377/api/Employee/UpdateEmployee", jsonBody);

                return RedirectToAction("List");
            }
            //model = _employeeModelFactory.PrepareEmployeeModel(model, employee);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Current = "Employee";

            var employeeResponse = await _commonAPIService.DeleteEntityAsync("https://localhost:44377/api/Employee/DeleteEmployee", id);
            if (employeeResponse.IsSuccessStatusCode)
                return Json(new { });

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> ImportExcel(IFormFile importexcelfile)
        {
            var stream = new MemoryStream();
            await importexcelfile.CopyToAsync(stream);
            stream.Position = 0;

            using (var package = new XLWorkbook(stream))
            {
                var worksheet = package.Worksheet(1); // Get the first worksheet

                var headers = worksheet.Row(1).Cells().Select(c => c.Value.ToString()).ToList();

                foreach (var row in worksheet.RowsUsed().Skip(1)) // Assuming the first row is headers
                {
                    var employeeModel = new EmployeeModel
                    {
                        FirstName = row.Cell(2).GetValue<string>(),
                        LastName = row.Cell(3).GetValue<string>(),
                        Email = row.Cell(4).GetValue<string>(),
                        Number = row.Cell(5).GetValue<string>(),
                        IpAddress = row.Cell(6).GetValue<string>(),
                        MaritalStatus = row.Cell(7).GetValue<string>(),
                        FamilyMembers = row.Cell(8).GetValue<string>(),
                        SalaryPackage = row.Cell(9).GetValue<string>(),
                        LoansEnrolment = row.Cell(10).GetValue<string>(),
                        ProgramsEnrolment = row.Cell(11).GetValue<string>(),
                        EmployeeID = row.Cell(12).GetValue<string>(),
                        DeskTopIP = row.Cell(13).GetValue<string>(),
                        LaptopIP = row.Cell(14).GetValue<string>(),
                        MobileSIM = row.Cell(15).GetValue<string>(),
                        WiFiIP = row.Cell(16).GetValue<string>(),
                        Gender = row.Cell(17).GetValue<string>(),
                        DepartmentID = int.Parse(row.Cell(18).GetValue<string>()),
                        BookingStartDate = row.Cell(19).GetValue<DateTime>(),
                        BookingEndDate = row.Cell(20).GetValue<DateTime>()
                    };

                    var jsonBody = JsonConvert.SerializeObject(employeeModel);
                    var response = await _commonAPIService.ApiPostAsync("https://localhost:44377/api/Employee/CreateEmployee", jsonBody);
                }
            }

            return RedirectToAction("List");
        }

        #endregion
    }
}
