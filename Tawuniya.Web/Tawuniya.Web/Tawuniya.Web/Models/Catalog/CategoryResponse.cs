namespace Tawuniya.Web.Models.Catalog
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentCategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
