namespace Tawuniya.Web.Models.Deparments
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
