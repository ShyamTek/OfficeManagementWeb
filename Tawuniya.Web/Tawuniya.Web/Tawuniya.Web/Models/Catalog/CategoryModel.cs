using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tawuniya.Web.Models.Catalog
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            AvaliableCategories = new List<SelectListItem>();
        }
        public int CategoryId { get; set; }
        public IList<SelectListItem> AvaliableCategories { get; set; }
    }
}
