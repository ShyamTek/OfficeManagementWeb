namespace Tawuniya.Core.Domain.Employees
{
    public partial class Employee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Number { get; set; }
        public string? IpAddress { get; set; }
        public string? MaritalStatus { get; set; }
        public string? FamilyMembers { get; set; }
        public string? SalaryPackage { get; set; }
        public string? LoansEnrolment { get; set; }
        public string? ProgramsEnrolment { get; set; }
        public string? EmployeeID { get; set; }
        public string? DeskTopIP { get; set; }
        public string? LaptopIP { get; set; }
        public string? MobileSIM { get; set; }
        public string? WiFiIP { get; set; }
        public string? Gender { get; set; }
        public int DepartmentID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
