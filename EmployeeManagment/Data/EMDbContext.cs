using EmployeeManagment.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Data;

public class EMDbContext : DbContext
{
    public EMDbContext(DbContextOptions<EMDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Relationships
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Department>()
            .HasIndex(d => d.Name)
            .IsUnique();
    }
}