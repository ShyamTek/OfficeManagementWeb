using Tawuniya.Core.Domain.Departments;

namespace Tawuniya.Services.Departments
{
    public partial interface IDepartmentService
    {
        /// <summary>
        /// get department by id async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>department</returns>
        Task<Department> GetDepartmentByIdAsync(int id);

        /// <summary>
        /// insert department async
        /// </summary>
        /// <param name="department">department</param>
        Task InsertDepartmentAsync(Department department);

        /// <summary>
        /// update department async
        /// </summary>
        /// <param name="department">department</param>
        Task UpdateDepartmentAsync(Department department);
        
        /// <summary>
        /// delete department async
        /// </summary>
        /// <param name="department">department</param>
        Task DeleteDepartmentAsync(Department department);
    }
}
