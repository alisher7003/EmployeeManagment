//using EmployeeManagment.Data;
//using EmployeeManagment.Entities;
//using EmployeeManagment.Services;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeManagment.Repositories;

//public class EmployeeRespository<T>(EMDbContext dbContext)
//{ 
//    public async Task<IQueryable<Employee>> GetByIdAsync(int Id)
//    {
//        dbContext.Database.ExecuteSqlRaw("Select INTO Employees set Name = 'Toshmat' WHERE Id = 1", Id);
//        var employee = dbContext.Employees.FirstOrDefault(x => x.Id == Id);
//        employee.Name = "Toshmat";
//        //dbContext.Employees.Update(employee);
//        await dbContext.SaveChangesAsync();

//        // bulk data update or delete.
//        await dbContext.Employees.ExecuteUpdateAsync(x => new Employee { Name = "Toshmat"});

//        // Compiled query
//    }
//}
