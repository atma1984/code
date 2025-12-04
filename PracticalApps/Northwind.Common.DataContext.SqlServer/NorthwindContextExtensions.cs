using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
namespace Packt.Shared;
public static class NorthwindContextExtensions
{

public static IServiceCollection AddNorthwindContext(
this IServiceCollection services, string connectionString =
"Data Source = NKMS0310232\\MSSQLSERVER_CORE; Initial Catalog = Northwind; User ID = sa; Password = VjzLjxf2020!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;")
    {
        services.AddDbContext<NorthwindContext>(options =>
        options.UseSqlServer(connectionString).UseLoggerFactory(new ConsoleLoggerFactory()));
        return services;
    }
}
