using EmployeeManagment.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Data;

public class EMDbContext : DbContext
{
    public EMDbContext(DbContextOptions<EMDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}