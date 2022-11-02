using ElectricityManagement.Application.IRepositories;
using ElectricityManagement.Infrastructure.Data.DbContext;
using ElectricityManagement.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectricityManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IElectricityDataRepository, ElectricityDataRepository>();

        services.AddDbContext<ElectricityManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ElectricityManagementConnectionString")));
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(ElectricityManagementDbContext)) is ElectricityManagementDbContext electricityManagementDbContext))
        {
            electricityManagementDbContext.Database.EnsureCreated();
            electricityManagementDbContext.Dispose();
        }

        return services;
    }
}
