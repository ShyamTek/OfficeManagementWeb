namespace Tawuniya.Core.Domain.Employees
{
    public partial class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string DeskIp { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
