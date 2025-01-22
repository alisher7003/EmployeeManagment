using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagment.Data
{
    public static class DatabaseExtensions
    {
        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                using var dbContext = services.GetRequiredService<EMDbContext>();
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
