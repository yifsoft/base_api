using Application.Dto;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers;


public class AccountController : Controller
{

    private readonly IUserService userService;

    public AccountController(IUserService userService)
    {
        this.userService = userService;
    }

    #region Login

    #region Get
    [HttpGet("Login")]
    public IActionResult Login(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return User.Identity.IsAuthenticated ? Redirect("/") : View();

    }


    #endregion

    #region Post
    [HttpPost("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string? returnUrl)
    {
        if (!ModelState.IsValid)
            return View(model);


        var result = await userService.Login(model);
        if (!result.Success)
            return View(model);


        var accessToken = await userService.GetAccessToken(result.Data);

        accessToken.Data.AuthenticationProperties.IsPersistent = model.RememberMe;
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, accessToken.Data.ClaimsPrincipal, accessToken.Data.AuthenticationProperties);

        return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
    }
    #endregion

    #endregion

    #region Logout
    #region Post
    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
    #endregion
    #endregion
}