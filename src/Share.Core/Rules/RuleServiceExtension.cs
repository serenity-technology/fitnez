using Microsoft.Extensions.DependencyInjection;

namespace Share;

public static class RuleServiceExtension
{
    public static IServiceCollection AddRules(this IServiceCollection services)
    {
        services.AddTransient<RuleBuilder>();
        return services;
    }
}