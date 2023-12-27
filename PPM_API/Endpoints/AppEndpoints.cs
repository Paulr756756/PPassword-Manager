using Microsoft.AspNetCore.Mvc;
using PPM_API.Extensions;
using PPM_API.Models;

namespace PPM_API.Endpoints;

/// <summary>
///     Contains all the endpoints of the API
/// </summary>
public static class AppEndpoints
{
    /// <summary>
    ///     GET endpoints
    /// </summary>
    /// <param name="app">The Web Application</param>
    public static void GETEndpoints(WebApplication app)
    {
        app.MapGet("/saved-sites", async (HttpContext ctx) =>
        {
            var logger = HttpCtxExtensions.GetLogger<Program>(ctx);
            var services = HttpCtxExtensions.GetAppServices(ctx);
            var user = await services.FetchUser(ctx);
            if (user is null) return Results.BadRequest("No such user present in database");
            try
            {
                var list = user.SavedSites;
                return Results.Ok(list);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return Results.BadRequest("An error occurred");
            }
        }).RequireAuthorization();
    }

    /// <summary>
    ///     For handling POST requests
    /// </summary>
    /// <param name="app">The Web Application</param>
    public static void POSTEndpoints(WebApplication app)
    {
        app.MapPost("/gen-pwd", (HttpContext ctx, [FromBody] ApiRequest request) =>
        {
            var services = HttpCtxExtensions.GetAppServices(ctx);
            var logger = HttpCtxExtensions.GetLogger<Program>(ctx);
            try
            {
                if (string.IsNullOrEmpty(request.MasterKey)) return Results.BadRequest("Master key is null");
                var result = services.GeneratePassword(request.Title, request.MasterKey);
                return Results.Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return Results.BadRequest("An error occurred");
            }
        });

        app.MapPost("/auth/gen-pwd", async (HttpContext ctx, [FromBody] ApiRequest request) =>
        {
            var logger = HttpCtxExtensions.GetLogger<Program>(ctx);
            var services = HttpCtxExtensions.GetAppServices(ctx);
            await using var dbContext = HttpCtxExtensions.GetDbContext(ctx);
            try
            {
                var user = await services.FetchUser(ctx);
                if (user is null) return Results.BadRequest($"User with email: {ctx.User.Identity?.Name} not found");

                var masterKey = user.PasswordHash;
                if (masterKey is null) throw new NotImplementedException();

                var userSavedSites = user.SavedSites;
                var title = request.Title;
                if (!userSavedSites.Contains(title))
                {
                    userSavedSites.Add(title);
                    await dbContext.SaveChangesAsync();
                }

                var result = services.GeneratePassword(title, masterKey);
                return Results.Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return Results.BadRequest("An error occurred");
            }
        }).RequireAuthorization();
    }

    /// <summary>
    ///     For handling DELETE requests
    /// </summary>
    /// <param name="app">The Web Application</param>
    public static void DELETEEndpoints(WebApplication app)
    {
        app.MapDelete("/auth/delete-site", async (HttpContext ctx, [FromBody] ApiRequest request) =>
        {
            var logger = HttpCtxExtensions.GetLogger<Program>(ctx);
            var services = HttpCtxExtensions.GetAppServices(ctx);
            await using var dbContext = HttpCtxExtensions.GetDbContext(ctx);
            try
            {
                var site = request.Title;
                var user = await services.FetchUser(ctx);
                if (user is null) return Results.BadRequest($"User with email: {ctx.User.Identity?.Name} not found");
                var result = user.SavedSites.Remove(site);
                await dbContext.SaveChangesAsync();
                if (!result)
                {
                    logger.LogInformation($"Unable to delete site: {site}");
                    return Results.BadRequest("Couldn't delete site");
                }

                return Results.Ok($"Deleted site {site}");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return Results.BadRequest("An error occurred");
            }
        }).RequireAuthorization();

        app.MapDelete("/auth/clear-sites", async (HttpContext ctx) =>
        {
            var logger = HttpCtxExtensions.GetLogger<Program>(ctx);
            var services = HttpCtxExtensions.GetAppServices(ctx);
            await using var dbContext = HttpCtxExtensions.GetDbContext(ctx);
            try
            {
                var user = await services.FetchUser(ctx);
                if (user is null) return Results.BadRequest($"User with email: {ctx.User.Identity?.Name} not found");

                user.SavedSites.Clear();
                await dbContext.SaveChangesAsync();
                return Results.Ok("Cleared all your saved sites");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return Results.BadRequest("An error occurred");
            }
        }).RequireAuthorization();
    }
}