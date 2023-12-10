using System.Security.Cryptography;
using System.Text;
using PPM_API.Extensions;
using PPM_API.Models;

namespace PPM_API.Services;

public interface IAppServices
{
    public string GeneratePassword(string siteTitle, string masterKey);
    public Task<User?> FetchUser(HttpContext ctx);
}

/// <summary>
///     Contains all the application services.
/// </summary>
public class AppServices : IAppServices
{
    /// <summary>
    ///     Returns a stronger password
    /// </summary>
    /// <param name="siteTitle">The title of the website. Would serve as the input key</param>
    /// <param name="masterKey">User's master key. Would serve as the salt</param>
    /// <returns>Generated password string</returns>
    public string GeneratePassword(string siteTitle, string masterKey)
    {
        using var shaM = SHA1.Create();
        var value = string.Concat(siteTitle, masterKey);
        var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(value!));
        var result = Convert.ToBase64String(hash);
        return result;
    }

    /// <summary>
    ///     Fetch User from the DB with given context
    /// </summary>
    /// <param name="ctx">Thee Http Context</param>
    /// <returns>User</returns>
    public async Task<User?> FetchUser(HttpContext ctx)
    {
        var userManager = HttpCtxExtensions.GetUserManager(ctx);
        var user = await userManager.GetUserAsync(ctx.User);
        return user;
    }
}