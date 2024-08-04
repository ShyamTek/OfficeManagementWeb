using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tawuniya.Services.Common;
using Tawuniya.Web.Models.Deparments;

namespace Tawuniya.Web.Controllers
{
    public class DepartmentController : Controller
    {
        #region Fields

        private readonly ICommonAPIService _commonAPIService;

        #endregion

        #region Ctor

        public DepartmentController(ICommonAPIService commonAPIService)
        {
            _commonAPIService = commonAPIService;
        }

        #endregion

        #region Methods

        public IActionResult List()
        {
            ViewBag.Current = "Department";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentList()
        {
            var response = await _commonAPIService.EntityListAsync("https://localhost:44377/api/Department/DepartmentList");
            var users = JsonConvert.DeserializeObject<IList<DepartmentResponse>>(response);


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

        #endregion
    }
}
