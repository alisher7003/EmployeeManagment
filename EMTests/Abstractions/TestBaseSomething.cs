
using EmployeeManagment.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EMTests.Abstractions;

public class TestBaseSomething : IAsyncLifetime
{
    public EMDbContext dbContext = default!;
    public HttpClient client = default!;
    private IServiceProvider serviceProvider = default!;
    private WebApplicationFactory<Program> factory = default!;

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

                    // Register a new DbContext for testing
                    var connectionString = GetTestDatabaseConnectionString();
                    services.AddDbContext<EMDbContext>(options =>
                        options.UseNpgsql(connectionString));
                });

                builder.UseEnvironment("Testing");
            });

        // Create HttpClient for testing
        client = factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        // Clean up the test database
        if (dbContext != null)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        client?.Dispose();
        factory?.Dispose();
    }

    protected T GetService<T>()
    {
        return serviceProvider.GetRequiredService<T>();
    }

    private string GetTestDatabaseConnectionString()
    {
        var dbName = $"TestDb_{Guid.NewGuid()}";
        return $"Host=localhost;Port=5432;Database={dbName};Username=postgres;Password=password";
    }
}
