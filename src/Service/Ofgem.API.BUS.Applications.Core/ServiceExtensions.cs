using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notify.Client;
using Notify.Interfaces;
using Ofgem.API.BUS.Applications.Core.AutoMapper.Profiles;
using Ofgem.API.BUS.Applications.Core.Configuration;
using Ofgem.API.BUS.Applications.Core.FluentValidation;
using Ofgem.API.BUS.Applications.Core.Interfaces;

namespace Ofgem.API.BUS.Applications.Core;

/// <summary>
/// Service Extensions to add Ofgem.API.BUS.Applications DI implementations
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// IServiceCollection Extension method to configure and add Ofgem.API.BUS.Applications DI implementations
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationsConfigurations(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapperConfiguration();
        services.AddFluentValidationConfiguration();
        services.AddGovNotifyServices(config);

        var applicationsApiConfig = config.GetRequiredSection("ApplicationsApi").Get<ApplicationsApiConfiguration>();

        services.AddSingleton(applicationsApiConfig);
        services.AddTransient<IPropertyConsentService, PropertyConsentService>();
        services.AddTransient<IBusinessAccountsService, BusinessAccountsService>();
        services.AddTransient<IApplicationsService, ApplicationsService>();

        return services;
    }

    /// <summary>
    /// Adds the service configuration for Automapper
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    private static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        var type = typeof(CreateApplicationRequestProfiler);
        var assemblyFromType = type.Assembly;
        services.AddAutoMapper(assemblyFromType);

        return services;
    }

    /// <summary>
    /// Adds the configuration for Fluent validation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    private static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidation(fv =>
        {
            fv.DisableDataAnnotationsValidation = true;
            fv.RegisterValidatorsFromAssemblyContaining<CreateApplicationRequestValidator>();
        });

        return services;
    }

    /// <summary>
    /// Adds services for using Gov Notify.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    private static IServiceCollection AddGovNotifyServices(this IServiceCollection services, IConfiguration config)
    {
        var govNotifyConfig = config.GetRequiredSection("GovNotify").Get<GovNotifyConfiguration>();

        services.AddSingleton(govNotifyConfig);
        services.AddTransient<IAsyncNotificationClient>(s => new NotificationClient(govNotifyConfig.ApiKey));
        services.AddTransient<IEmailService, NotifyEmailService>();

        return services;
    }
}
