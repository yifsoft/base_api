using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Domain.Common.Models;

public class AccessToken
{
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
    public AuthenticationProperties AuthenticationProperties { get; set; }
}

