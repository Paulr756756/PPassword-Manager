using Microsoft.AspNetCore.Identity;

namespace PPM_API.Models;

/// <summary>
///     User class
/// </summary>
public class User : IdentityUser
{
    public List<string> SavedSites { get; set; } = new();
}

/// <summary>
///     Request body for HTTP requests
/// </summary>
public record ApiRequest(string Title, string? MasterKey);