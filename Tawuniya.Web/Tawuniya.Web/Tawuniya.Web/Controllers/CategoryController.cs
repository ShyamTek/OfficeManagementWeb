using Microsoft.AspNetCore.Mvc;

namespace Tawuniya.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
