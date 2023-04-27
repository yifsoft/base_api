using Application.Dto;
using Application.Interfaces.Services;
using Application.Utilities.Helpers;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Admin.Controllers;

public class PostsController : BaseController
{
    private readonly IWebHostEnvironment environment;
    private readonly IToastNotification notification;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    public PostsController(IToastNotification notification, IPostService postService, IWebHostEnvironment environment, ICategoryService categoryService)
    {
        this.notification = notification;
        this.postService = postService;
        this.environment = environment;
        this.categoryService = categoryService;
    }

    #region Index
    public async Task<IActionResult> Index(Language? lang = null)
    {
        SetLanguageSelectList(lang);
        var posts = await postService.GetListAsync(lang);
        return View(posts.Data);
    }
    #endregion

    #region Preview
    [HttpGet("Posts/Preview/{slug}")]
    public async Task<IActionResult> Preview(string slug)
    {
        var posts = await postService.GetBySlugAsync(slug);

        if (posts.Success)
            return View(posts.Data);

        notification.AddErrorToastMessage(posts.Message);
        return RedirectToAction("Index");

    }
    #endregion

    #region Add
    public IActionResult Add()
    {
        SetLanguageSelectList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostDto model)
    {
        SetLanguageSelectList(model.Language);
        await SetCategorySelectList(categoryService, model.CategoryId);

        var result = await postService.AddAsync(model);

        if (result.Success)
            return RedirectToAction("Index");

        notification.AddErrorToastMessage(result.Message);
        return View(model);
    }
    #endregion

    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        SetLanguageSelectList();
        var result = await postService.GetAsync(id);

        if (result.Success)
        {
            await SetCategorySelectList(categoryService, result.Data.CategoryId, result.Data.Language);
            return View(result.Data);
        }

        notification.AddErrorToastMessage(result.Message);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostDto model)
    {
        SetLanguageSelectList(model.Language);
        await SetCategorySelectList(categoryService, model.CategoryId, model.Language);

        var result = await postService.UpdateAsync(model);

        if (result.Success)
            return RedirectToAction("Index");

        notification.AddErrorToastMessage(result.Message);
        return View(model);
    }
    #endregion

    #region Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await postService.DeleteAsync(id);

        if (result.Success)
            notification.AddSuccessToastMessage(result.Message);
        else
            notification.AddErrorToastMessage(result.Message);

        return RedirectToAction("Index");
    }
    #endregion

    #region Image
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var result = await Image.SaveImageToFile(file, environment.WebRootPath);

        if (result.Success)
            return Ok(new { location = "\\images\\" + result.Data });

        notification.AddErrorToastMessage(result.Data);
        return Json(new { error = result.Message });
    }

    public IActionResult ImageList()
    {
        var files = Directory.GetFiles(environment.WebRootPath + "\\images");

        var res = (from item in files
                   select new { title = Path.GetFileName(item), value = "\\images\\" + Path.GetFileName(item) }).ToList();

        return Json(res);
    }


    [HttpGet]
    public IActionResult GetImage(string image)
    {
        var result = Image.GetByteImage(image);

        if (result.Success)
            return File(image, "image/jpeg");

        return BadRequest(result.Message);
    }
    #endregion
}
