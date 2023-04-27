using Application.Interfaces.Services;
using Domain.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Infrastructure.Services;

public class TokenManager : ITokenService
{
    public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
        identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

        foreach (var role in operationClaims)
            identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));


        var props = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTime.Now.AddDays(15)
        };

        var principal = new ClaimsPrincipal(identity);

        return new AccessToken
        {
            ClaimsPrincipal = principal,
            AuthenticationProperties = props
        };
    }
}

