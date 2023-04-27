using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Resource;

namespace Admin.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IStringLocalizer<SharedResource> localizer;

    public HomeController(IStringLocalizer<SharedResource> localizer)
    {
        this.localizer = localizer;
    }

    public IActionResult Index()
    {
        ViewBag.Message = localizer[Messages.Error.WrongCredential];
        return View();
    }
}
