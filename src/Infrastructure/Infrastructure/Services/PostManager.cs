using Application.Business;
using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utilities.Results;
using Application.ViewModel;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Resource;
using System.Security.Claims;

namespace Infrastructure.Services;

public class PostManager : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly IMapper mapper;
    private readonly IStringLocalizer<SharedResource> localizer;
    private readonly IHttpContextAccessor httpContextAccessor;


    public PostManager(IPostRepository postRepository, IMapper mapper, IStringLocalizer<SharedResource> localizer, IHttpContextAccessor httpContextAccessor)
    {
        this.postRepository = postRepository;
        this.mapper = mapper;
        this.localizer = localizer;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IResult> AddAsync(PostDto model)
    {
        try
        {
            var rules = BusinessRules.Run(CheckPostSlugExist(model.Slug));
            if (!rules.Success)
                return new ErrorResult(rules.Message);

            var entity = mapper.Map<Post>(model);
            entity.CreatedAt = DateTime.Now;
            entity.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await postRepository.AddAsync(entity);

            return result ? new SuccessResult() : new ErrorResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }
    }

    public async Task<IResult> UpdateAsync(PostDto model)
    {
        try
        {
            var entity = await postRepository.FirstOrDefaultAsync(x => x.Id == model.Id && x.DeletedAt == null);

            if (entity == null)
                return new ErrorResult("Post blunamadı!");

            var rules = BusinessRules.Run(CheckPostSlugExist(model.Slug, entity.Slug));
            if (!rules.Success)
                return new ErrorResult(rules.Message);


            entity = mapper.Map<Post>(model);
            entity.UpdatedAt = DateTime.Now;

            var result = await postRepository.UpdateAsync(entity);

            return result ? new SuccessResult() : new ErrorResult();
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
            var entity = await postRepository.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

            if (entity == null)
                return new ErrorResult("Post blunamadı!");

            entity.UpdatedAt = DateTime.Now;
            entity.DeletedAt = DateTime.Now;
            var result = await postRepository.UpdateAsync(entity);

            return result ? new SuccessResult("Silindi") : new ErrorResult("Silirken bir sorun oluştu");
        }
        catch (Exception e)
        {
            return new ErrorResult(e.Message);
        }

    }

    public async Task<IDataResult<PostViewModel>> GetBySlugAsync(string slug)
    {
        try
        {
            var result = await postRepository.FirstOrDefaultAsync(x => x.Slug == slug && x.DeletedAt == null, true, x => x.User);

            if (result != null)
                return new SuccessDataResult<PostViewModel>(mapper.Map<PostViewModel>(result));

        }
        catch (Exception e)
        {
            return new ErrorDataResult<PostViewModel>(e.Message);
        }

        return new ErrorDataResult<PostViewModel>();
    }
    public async Task<IDataResult<PostDto>> GetAsync(int id)
    {
        try
        {
            var result = await postRepository.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null, true, x => x.User);

            if (result != null)
                return new SuccessDataResult<PostDto>(mapper.Map<PostDto>(result));

        }
        catch (Exception e)
        {
            return new ErrorDataResult<PostDto>(e.Message);
        }

        return new ErrorDataResult<PostDto>();
    }

    public async Task<IDataResult<IEnumerable<PostViewModel>>> GetListAsync(Language? lang = null)
    {
        try
        {
            var result = await postRepository.GetListAsync(lang == null ? x => x.DeletedAt == null : x => x.Language == lang && x.DeletedAt == null);

            if (result != null && result.Count > 0)
                return new SuccessDataResult<IEnumerable<PostViewModel>>(mapper.Map<IEnumerable<PostViewModel>>(result));

        }
        catch (Exception e)
        {
            return new SuccessDataResult<IEnumerable<PostViewModel>>(e.Message);
        }

        return new SuccessDataResult<IEnumerable<PostViewModel>>();
    }

    #region Methods
    private IResult CheckPostSlugExist(string value, string currentValue = "")
    {
        if (!string.IsNullOrEmpty(currentValue) && value == currentValue)
            return new SuccessResult();

        var result = postRepository.AsQueryable().Any(x => x.Slug == value);
        return result ? new ErrorResult("Bu url adresi sistemde mevcut!") : new SuccessResult();
    }
    #endregion
}
