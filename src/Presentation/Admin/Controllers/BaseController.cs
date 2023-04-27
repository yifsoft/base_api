using Application.Interfaces.Services;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Controllers;

public class BaseController : Controller
{
    public void SetLanguageSelectList(Language? selectedLanguage = null)
    {
        var list = new List<SelectListItem>();

        foreach (Language language in (Language[])Enum.GetValues(typeof(Language)))
            list.Add(new SelectListItem() { Value = language.ToString(), Text = language == Language.en ? "İngilizce" : "Türkçe", Selected = language == selectedLanguage });

        ViewData["languageSelectList"] = list;
    }

    public async Task SetCategorySelectList(ICategoryService categoryService, int? selectedId = null, Language? language = null)
    {
        var list = new List<SelectListItem>();
        var result = await categoryService.GetListAsync(language);

        if (result.Success)
            list = result.Data.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == selectedId }).ToList();


        ViewData["categorySelectList"] = list;
    }
}
