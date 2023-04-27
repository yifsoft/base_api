using Application.Business;
using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utilities.Results;
using Application.ViewModel;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Microsoft.Extensions.Localization;
using Resource;

namespace Infrastructure.Services;

public class CategoryManager : ICategoryService
{
    private readonly IMapper mapper;
    private readonly ICategoryRepository categoryRepository;
    private readonly IStringLocalizer<SharedResource> localizer;

    public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, IStringLocalizer<SharedResource> localizer)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<IResult> AddAsync(CategoryDto model)
    {
        try
        {
            var rules = BusinessRules.Run(CheckCategoryExist(model.Name), CheckCategorySlugExist(model.Slug));
            if (!rules.Success) return new ErrorResult(rules.Message);

            var entity = mapper.Map<Category>(model);
            entity.CreatedAt = DateTime.Now;

            var result = await categoryRepository.AddAsync(entity);

            return result ? new SuccessResult("Kategori eklendi.") : new ErrorResult("Kategori eklenirken bir sorun oluştu! Lütfen tekrar deneyiniz.");
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }
    }

    public async Task<IResult> UpdateAsync(CategoryDto model)
    {
        try
        {
            var entity = await categoryRepository.FirstOrDefaultAsync(x => x.Id == model.Id && x.DeletedAt == null);

            var rules = BusinessRules.Run(CheckCategoryExist(model.Name, entity.Name), CheckCategorySlugExist(model.Slug, entity.Slug));
            if (!rules.Success) return new ErrorResult(rules.Message);

            entity = mapper.Map<Category>(model);
            entity.UpdatedAt = DateTime.Now;

            var result = await categoryRepository.UpdateAsync(entity);

            return result ? new SuccessResult("Güncellendi.") : new ErrorResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        try
        {
            var entity = await categoryRepository.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

            if (entity == null)
                return new ErrorResult("Kategori bulunamadı");

            entity.UpdatedAt = DateTime.Now;
            entity.DeletedAt = DateTime.Now;

            var result = await categoryRepository.UpdateAsync(entity);

            return result ? new SuccessResult("Silindi") : new ErrorResult("Silirken bir sorun oluştu");
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }
    }

    public async Task<IDataResult<CategoryDto>> GetAsync(int id)
    {
        try
        {
            var result = await categoryRepository.GetByIdAsync(id);

            if (result != null)
                return new SuccessDataResult<CategoryDto>(mapper.Map<CategoryDto>(result));

        }
        catch (Exception e)
        {
            return new ErrorDataResult<CategoryDto>(e.Message);
        }

        return new ErrorDataResult<CategoryDto>("Kategori bulunamadı!");
    }

    public async Task<IDataResult<IEnumerable<CategoryViewModel>>> GetListAsync(Language? lang = null)
    {
        try
        {
            var result = await categoryRepository.GetListAsync(lang == null ? x => x.DeletedAt == null : x => x.Language == lang && x.DeletedAt == null, true, x => x.OrderBy(x => x.Name), x => x.Posts);

            if (result != null && result.Count > 0)
                return new SuccessDataResult<IEnumerable<CategoryViewModel>>(mapper.Map<IEnumerable<CategoryViewModel>>(result));
        }
        catch (Exception e)
        {
            return new ErrorDataResult<IEnumerable<CategoryViewModel>>(e.Message);
        }

        return new ErrorDataResult<IEnumerable<CategoryViewModel>>();
    }

    #region Methods
    private IResult CheckCategoryExist(string value, string currentValue = "")
    {
        if (!string.IsNullOrEmpty(currentValue) && value == currentValue)
            return new SuccessResult();

        var result = categoryRepository.AsQueryable().Any(x => x.Name == value);
        return result ? new ErrorResult("Kategori adı kullanımda!") : new SuccessResult();
    }

    private IResult CheckCategorySlugExist(string value, string currentValue = "")
    {
        if (!string.IsNullOrEmpty(currentValue) && value == currentValue)
            return new SuccessResult();

        var result = categoryRepository.AsQueryable().Any(x => x.Slug == value);
        return result ? new ErrorResult("Kategori adı kullanımda!") : new SuccessResult();
    }

    #endregion
}
