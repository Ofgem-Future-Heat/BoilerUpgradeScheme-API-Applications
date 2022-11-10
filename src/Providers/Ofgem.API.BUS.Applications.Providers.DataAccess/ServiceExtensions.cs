using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ofgem.API.BUS.Applications.Providers.DataAccess.Interfaces;
using Ofgem.Lib.BUS.AuditLogging.Interfaces;

namespace Ofgem.API.BUS.Applications.Providers.DataAccess;

public static class ServiceExtensions
{
    /// <summary>
    /// Configuration to add ApplicationsDBContext to applications api
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationsDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationsDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationsConnection"),
            assembly => assembly.MigrationsAssembly(typeof(ApplicationsDBContext).Assembly.FullName)));

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IAuditLogsDbContext, ApplicationsDBContext>();

        services.AddTransient<IApplicationsProvider, ApplicationsProvider>();

        return services;
    }
}
