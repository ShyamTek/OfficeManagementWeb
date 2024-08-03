using Tawuniya.Core.Domain.Departments;
using Tawuniya.Data;

namespace Tawuniya.Services.Departments
{
    public partial class DepartmentService : IDepartmentService
    {
        #region Fields

        private readonly TawuniyaDBContext _context;

        #endregion

        #region Ctor

        public DepartmentService(TawuniyaDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// get department by id async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>department</returns>
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Department.FindAsync(id);
        }

        /// <summary>
        /// insert department async
        /// </summary>
        /// <param name="department">department</param>
        public async Task InsertDepartmentAsync(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update department async
        /// </summary>
        /// <param name="department">department</param>
        public async Task UpdateDepartmentAsync(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _context.Department.Update(department);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete department async
        /// </summary>
        /// <param name="department">department</param>
        public async Task DeleteDepartmentAsync(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
