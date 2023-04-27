using Application.Dto;
using Application.Utilities.Results;
using Application.ViewModel;
using Domain.Common.Enums;

namespace Application.Interfaces.Services;
public interface IPostService
{
    Task<IResult> AddAsync(PostDto model);
    Task<IResult> UpdateAsync(PostDto model);
    Task<IResult> DeleteAsync(int id);
    Task<IDataResult<PostDto>> GetAsync(int id);
    Task<IDataResult<PostViewModel>> GetBySlugAsync(string slug);
    Task<IDataResult<IEnumerable<PostViewModel>>> GetListAsync(Language? lang);
}
