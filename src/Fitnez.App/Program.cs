using Fitnez;
using Fitnez.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.AddKeyedNpgsqlDataSource(name: "adm");
builder.AddKeyedNpgsqlDataSource(name: "db", configureDataSourceBuilder: options =>
{
    options.MapEnum<Gender>();
    options.MapEnum<PersonStatus>();
});
builder.Services.AddDataAccess();

builder.Services.AddAuthentication("FitnezOidc")
        .AddOpenIdConnect("FitnezOidc", options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            options.Authority = "https://localhost:5001";

            options.ClientId = "fitnez";
            options.ClientSecret = "D48A84AD-0256-49C8-92EA-6E0FD42F385A";

            options.ResponseType = OpenIdConnectResponseType.Code;
            options.ResponseMode = "query";

            options.GetClaimsFromUserInfoEndpoint = true;
            options.SaveTokens = true;

            options.MapInboundClaims = false;
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "roles";

            options.Scope.Add(OpenIdConnectScope.OfflineAccess);
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, "FitnezOidc");
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddCors();

// API
builder.Services.AddPersionEndpoints();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Fitnez.Client._Imports).Assembly);

app.MapGroup("/authentication").MapLoginAndLogout();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Database
var dataUp = app.Services.GetRequiredService<DataScriptBuilder>();
dataUp.Execute(Assembly.GetExecutingAssembly());

app.MapPersonEndpoints();

app.Run();