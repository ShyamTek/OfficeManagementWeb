using Microsoft.EntityFrameworkCore;
using Tawuniya.Core;
using Tawuniya.Core.Domain.Departments;
using Tawuniya.Core.Domain.Employees;
using Tawuniya.Core.Domain.Layout;
using Tawuniya.Core.Domain.Seats;
using Tawuniya.Core.Domain.Users;

namespace Tawuniya.Data
{
    public class TawuniyaDBContext : DbContext
    {
        public TawuniyaDBContext(DbContextOptions<TawuniyaDBContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPassword { get; set; }
        public DbSet<Department> Department {  get; set; }
        public DbSet<Employee> Employee {  get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Booking> Booking { get; set; }

        //public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        //{
        //    return base.Set<TEntity>();
        //}
    }
}
