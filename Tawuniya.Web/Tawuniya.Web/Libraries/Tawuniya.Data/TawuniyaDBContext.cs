using Microsoft.EntityFrameworkCore;
using Tawuniya.Core.Domain.Departments;
using Tawuniya.Core.Domain.Employees;
using Tawuniya.Core.Domain.Layouts;
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
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Polygon> Polygons { get; set; }
        public DbSet<PolygonCoordinate> PolygonCoordinates { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Polygon>()
                .HasMany(p => p.Coordinates)
                .WithOne(c => c.Polygon)
                .HasForeignKey(c => c.PolygonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Polygon)
                .WithMany()
                .HasForeignKey(b => b.PolygonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Polygon>()
                .HasOne(p => p.Layout)
                .WithMany()
                .HasForeignKey(p => p.LayoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
