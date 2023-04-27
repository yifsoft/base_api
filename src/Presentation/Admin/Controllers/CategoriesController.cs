using Application.Dto;
using Application.Interfaces.Services;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Admin.Controllers;

public class CategoriesController : BaseController
{
    private readonly ICategoryService categoryService;
    private readonly IToastNotification notification;

    public CategoriesController(ICategoryService categoryService, IToastNotification notification)
    {
        this.categoryService = categoryService;
        this.notification = notification;
    }

    #region Index
    public async Task<IActionResult> Index(Language? lang = null)
    {
        SetLanguageSelectList(lang);
        var categories = await categoryService.GetListAsync(lang);

        return View(categories.Data);
    }
    #endregion

    #region Add
    public IActionResult Add()
    {
        SetLanguageSelectList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryDto model)
    {
        SetLanguageSelectList(model.Language);

        if (!ModelState.IsValid)
            return View(model);

        var result = await categoryService.AddAsync(model);

        if (result.Success)
        {
            notification.AddSuccessToastMessage(result.Message);
            return RedirectToAction("Index");
        }

        notification.AddErrorToastMessage(result.Message);
        return View(model);

    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        SetLanguageSelectList();

        var result = await categoryService.GetAsync(id);

        if (result.Success)
            return View(result.Data);

        notification.AddErrorToastMessage(result.Message);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto model)
    {
        SetLanguageSelectList(model.Language);

        if (!ModelState.IsValid)
            return View(model);

        var result = await categoryService.UpdateAsync(model);

        if (result.Success)
        {
            notification.AddSuccessToastMessage(result.Message);
            return RedirectToAction("Index");
        }

        notification.AddErrorToastMessage(result.Message);
        return View(model);

    }
    #endregion

    #region Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await categoryService.DeleteAsync(id);

        if (result.Success)
            notification.AddSuccessToastMessage(result.Message);
        else
            notification.AddErrorToastMessage(result.Message);

        return RedirectToAction("Index");
    }
    #endregion

    #region JsonMethods
    public async Task<JsonResult> List(Language? lang = null)
    {
        var result = await categoryService.GetListAsync(lang);
        if (result.Success)
            return Json(result.Data.Select(x => new { x.Id, x.Name }));

        return Json(result.Message);
    }
    #endregion
}


