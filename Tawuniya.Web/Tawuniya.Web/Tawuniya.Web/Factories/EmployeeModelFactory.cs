using Tawuniya.Core.Domain.Employees;
using Tawuniya.Web.Models.Employees;

namespace Tawuniya.Web.Factories
{
    public partial class EmployeeModelFactory : IEmployeeModelFactory
    {
        #region Fields

        #endregion

        #region Ctor

        public EmployeeModelFactory()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// prepare employee model
        /// </summary>
        /// <param name="model">modle</param>
        /// <param name="employee">employee</param>
        /// <returns>employee model</returns>
        public EmployeeModel PrepareEmployeeModel(EmployeeModel? model, Employee? employee)
        {
            model ??= new EmployeeModel();
            if (employee != null)
            {
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.Email = employee.Email;
                model.Number = employee.Number;
                model.DepartmentID = employee.DepartmentID;
                model.IpAddress = employee.IpAddress;
                model.EmployeeID = employee.EmployeeID;
                model.FamilyMembers = employee.FamilyMembers;
                model.MaritalStatus = employee.MaritalStatus;
                model.SalaryPackage = employee.SalaryPackage;
                model.LoansEnrolment = employee.LoansEnrolment;
                model.ProgramsEnrolment = employee.ProgramsEnrolment;
                model.DeskTopIP = employee.DeskTopIP;
                model.LaptopIP = employee.LaptopIP;
                model.MobileSIM = employee.MobileSIM;
                model.WiFiIP = employee.WiFiIP;
                model.Gender = employee.Gender;
                model.BookingEndDate = employee.BookingEndDate;
                model.BookingStartDate = employee.BookingStartDate;
            }

            return model;
        }

        #endregion
    }
}
