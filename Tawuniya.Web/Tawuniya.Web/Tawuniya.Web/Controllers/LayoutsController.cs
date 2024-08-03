using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tawuniya.Core.Domain.Layouts;
using Tawuniya.Data;
using Tawuniya.Services.Layouts;
using Tawuniya.Web.Models;
using Tawuniya.Web.Models.Seats;

namespace Tawuniya.Web.Controllers
{
    public class LayoutsController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly TawuniyaDBContext _context;


        public LayoutsController(ILayoutService layoutService,
            TawuniyaDBContext context)
        {
            _layoutService = layoutService;
            _context = context;
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

        //[HttpPost]
        //public async Task<IActionResult> UploadLayout(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var filePath = Path.Combine("wwwroot/uploads", file.FileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }


        //        var layout = new Layout
        //        {
        //            Name = file.FileName,
        //            ImageUrl = "/uploads/" + file.FileName
        //        };
        //        await _layoutService.InsertLayoutAsync(layout);

        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}
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
                _context.Layouts.Add(layout);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        //[HttpGet]
        //public async Task<IActionResult> GetLayout(int id)
        //{
        //    var layout =await _layoutService.GetLayoutByIdAsync(id);

        //    return Json(layout.ImageUrl);
        //}
        [HttpGet]
        public IActionResult GetLayout(int id)
        {
            var layout = _context.Layouts
                .Where(l => l.Id == id)
                .Select(l => new { l.ImageUrl })
                .FirstOrDefault();
            return Json(layout);
        }
        // GET: Layouts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Layouts.ToListAsync());
        }
    }
}
