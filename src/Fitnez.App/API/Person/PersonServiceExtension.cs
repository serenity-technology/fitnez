namespace Fitnez;

public static class PersonServiceExtension
{
    public static IServiceCollection AddPersionEndpoints(this IServiceCollection services)
    {
        services.AddTransient<PersonRepository>();
        services.AddTransient<PersonCreateEndpoint>();

        return services;
    }

    public static void MapPersonEndpoints(this WebApplication application)
    {
        var group = application.MapGroup("api/person")
            .WithTags("Person")
            .WithOpenApi();

        group.MapPost("/", async
        (
            PersonCreateRequest request,
            HttpRequest httpRequest,
            ClaimsPrincipal user,
            [FromServices] PersonCreateEndpoint endpoint
        ) => await endpoint.ExecuteAsync(request, httpRequest, user))
        .WithSummary("Create Person");
    }
}