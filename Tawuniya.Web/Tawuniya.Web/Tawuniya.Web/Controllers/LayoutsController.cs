using Microsoft.AspNetCore.Mvc;
using Tawuniya.Core.Domain.Layout;
using Tawuniya.Services.Layouts;
using Tawuniya.Web.Models;

namespace Tawuniya.Web.Controllers
{
    public class LayoutsController : Controller
    {
        private readonly ILayoutService _layoutService;

        public LayoutsController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        [HttpGet]
        public async Task<IActionResult> ViewLayouts()
        {
            var layouts = await _layoutService.GetLayoutsAsync();
            var layoutViewModel = layouts.Select(layout => 
                new LayoutViewModel {
                    Id = layout.Id, Name = layout.Name 
                })
                .ToList();
            return View(layoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadLayout(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                var layout = new Layout
                {
                    Name = file.FileName,
                    ImageUrl = "/uploads/" + file.FileName
                };
                await _layoutService.InsertLayoutAsync(layout);

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetLayout(int id)
        {
            var layout =await _layoutService.GetLayoutByIdAsync(id);
                
            return Json(layout.ImageUrl);
        }
    }
}
