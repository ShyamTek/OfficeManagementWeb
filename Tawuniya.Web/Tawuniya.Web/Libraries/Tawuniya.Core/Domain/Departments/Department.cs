namespace Tawuniya.Core.Domain.Departments
{
    public partial class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
