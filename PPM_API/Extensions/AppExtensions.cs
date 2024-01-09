using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PPM_API.Data;
using PPM_API.Models;
using PPM_API.Services;

namespace PPM_API.Extensions;

/// <summary>
///     Dependency extensions.
/// </summary>
public static class DependencyExtensions
{
    /// <summary>
    ///     Register for DI
    /// </summary>
    /// <param name="builder">The web application builder</param>
    public static void RegisterDependencies(WebApplicationBuilder builder)
    {
        IServiceCollection? services = builder.Services;
        ConfigurationManager? configuration = builder.Configuration;
        string? dbConnString = configuration.GetConnectionString("Azure_SQL_CONNECTIONSTRING");
        
        services.AddCors(co => 
        {
            co.AddPolicy("myPolicy", pb => {
                 pb.WithOrigins("http://localhost:5173", "https://zealous-rock-06524e50f.4.azurestaticapps.net")
                    /*.WithOrigins(configuration["AllowedOrigins"]!)*/
                     .WithMethods("POST", "GET", "PUT", "DELETE")
                     .AllowAnyHeader()
                     .AllowCredentials();
            });
                
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(dbConnString);
        });
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddScoped<IAppServices, AppServices>();
    }

    /// <summary>
    ///     Default builder methods.
    /// </summary>
    /// <param name="app">The web application</param>
    public static void UseBuilderMethods(WebApplication app)
    {
        app.UseCors("myPolicy");

        app.MapIdentityApi<User>();
    }
}

/// <summary>
///     Custom Http Context extension methods.
/// </summary>
public static class HttpCtxExtensions
{
    /// <summary>
    ///     Gets logger
    /// </summary>
    /// <param name="ctx">The http context</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>ILogger</returns>
    public static ILogger<T> GetLogger<T>(HttpContext ctx)
    {
        return ctx.RequestServices.GetRequiredService<ILogger<T>>();
    }

    /// <summary>
    ///     Gets user manager
    /// </summary>
    /// <param name="ctx">The http context</param>
    /// <returns>User manager</returns>
    public static UserManager<User> GetUserManager(HttpContext ctx)
    {
        return ctx.RequestServices.GetRequiredService<UserManager<User>>();
    }

    /// <summary>
    ///     Gets the application services.
    /// </summary>
    /// <param name="ctx">The http context</param>
    /// <returns>IAppServices</returns>
    public static IAppServices GetAppServices(HttpContext ctx)
    {
        return ctx.RequestServices.GetRequiredService<IAppServices>();
    }

    /// <summary>
    ///     Gets the database context
    /// </summary>
    /// <param name="ctx">The http context</param>
    /// <returns>AppDbContext</returns>
    public static AppDbContext GetDbContext(HttpContext ctx)
    {
        return ctx.RequestServices.GetRequiredService<AppDbContext>();
    }
}