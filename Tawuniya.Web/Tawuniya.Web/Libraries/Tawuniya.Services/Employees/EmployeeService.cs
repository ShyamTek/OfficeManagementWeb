using Tawuniya.Core.Data;
using Tawuniya.Core.Domain.Employees;
using Tawuniya.Data;

namespace Tawuniya.Services.Employees
{
    public partial class EmployeeService : IEmployeeService
    {
        #region Fields

        private readonly TawuniyaDBContext _context;


        #endregion

        #region Ctor

        public EmployeeService(TawuniyaDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// get employee by id async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>employee</returns>
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        /// <summary>
        /// insert employee async
        /// </summary>
        /// <param name="employee">employee</param>
        public async Task InsertEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update employee async
        /// </summary>
        /// <param name="employee">employee</param>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete employee async
        /// </summary>
        /// <param name="employee">employee</param>
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
