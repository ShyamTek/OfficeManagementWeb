using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Tawuniya.Services.Common;
using Tawuniya.Web.Models.Catalog;

namespace Tawuniya.Web.Components
{
    public class CategorySelectorViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICommonAPIService _commonAPIService;

        #endregion

        #region Ctor

        public CategorySelectorViewComponent(ICommonAPIService commonAPIService)
        {
            _commonAPIService = commonAPIService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _commonAPIService.EntityListAsync("https://localhost:44377/api/Category/CategoryList");
            var categories = JsonConvert.DeserializeObject<IList<CategoryResponse>>(response);
            var model = new CategoryModel();

            foreach (var category in categories)
            {
                model.AvaliableCategories.Add(new SelectListItem 
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }

            return View(model);
        }


        #endregion
    }
}
