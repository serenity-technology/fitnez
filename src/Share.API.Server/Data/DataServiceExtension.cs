using Microsoft.Extensions.DependencyInjection;

namespace Share;

public static class DataServiceExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IDataSource, DataSource>();
        services.AddTransient<UnitOfWork>();
        services.AddSingleton<UnitOfWorkFactory>();
        services.AddTransient<DataScriptBuilder>();

        return services;
    }
}