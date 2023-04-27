using Application.Utilities.Security.Hashing;
using System.Security.Claims;

namespace Application.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(ClaimTypes.Email, HashingHelper.Decrypt(email)));
    }

    public static void AddFirstName(this ICollection<Claim> claims, string firstName)
    {
        claims.Add(new Claim(ClaimTypes.Name, firstName));
    }
    public static void AddLastname(this ICollection<Claim> claims, string lastName)
    {
        claims.Add(new Claim(ClaimTypes.Surname, lastName));
    }
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    }
    public static void AddRole(this ICollection<Claim> claims, string role)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }
    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
    }
}

