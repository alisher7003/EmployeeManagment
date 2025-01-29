using EmployeeManagment;
using EmployeeManagment.Data;
using EmployeeManagment.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMTests.Abstractions;

public class TestBaseSomething : IAsyncLifetime
{
    public HttpClient client = default!;
    public WebApplicationFactory<Program> factory = default!;
    public IServiceProvider serviceProvider = default!;

    public async Task InitializeAsync()
    {
        // Set up the application factory
        factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<EMDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);
                    var empDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IEmployeeService));

                    // Register a new DbContext for testing
                    var connectionString = GetTestDatabaseConnectionString();
                    services.AddDbContext<EMDbContext>(options =>
                        options.UseNpgsql(connectionString));
                });

                builder.UseEnvironment("Testing");
            });

        // Create HttpClient for testing
        client = factory.CreateClient();
        serviceProvider = factory.Services;
    }

    public async Task DisposeAsync()
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EMDbContext>();
        if (dbContext != null)
        {
            await dbContext.Database.CloseConnectionAsync();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }


        client?.Dispose();
        factory?.Dispose();
    }

    private string GetTestDatabaseConnectionString()
    {
        var dbName = $"TestDb";
        return $"Host=localhost;Port=5432;Database={dbName};Username=postgres;Password=password";
    }
}
