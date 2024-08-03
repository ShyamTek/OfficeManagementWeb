using Tawuniya.Core.Domain.Employees;

namespace Tawuniya.Services.Employees
{
    public interface IEmployeeService
    {
        /// <summary>
        /// get employee by id async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>employee</returns>
        Task<Employee> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// insert employee async
        /// </summary>
        /// <param name="employee">employee</param>
        Task InsertEmployeeAsync(Employee employee);

        /// <summary>
        /// update employee async
        /// </summary>
        /// <param name="employee">employee</param>
        Task UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// delete employee async
        /// </summary>
        /// <param name="employee">employee</param>
        Task DeleteEmployeeAsync(Employee employee);
    }
}
