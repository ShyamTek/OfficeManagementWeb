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
                model.BookingEndDate = employee.BookingEndDate;
                model.BookingStartDate = employee.BookingStartDate;
            }

            return model;
        }

        #endregion
    }
}
