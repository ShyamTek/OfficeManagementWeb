using Tawuniya.Core.Domain.Employees;
using Tawuniya.Web.Models.Employees;

namespace Tawuniya.Web.Factories
{
    public partial interface IEmployeeModelFactory
    {
        /// <summary>
        /// prepare employee model
        /// </summary>
        /// <param name="model">modle</param>
        /// <param name="employee">employee</param>
        /// <returns>employee model</returns>
        EmployeeModel PrepareEmployeeModel(EmployeeModel? model, Employee? employee);
    }
}
