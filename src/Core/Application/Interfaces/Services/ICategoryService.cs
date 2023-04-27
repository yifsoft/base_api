using Application.Dto;
using Application.Utilities.Results;
using Application.ViewModel;
using Domain.Common.Enums;

namespace Application.Interfaces.Services;
public interface ICategoryService
{
    Task<IResult> AddAsync(CategoryDto model);
    Task<IResult> UpdateAsync(CategoryDto model);
    Task<IResult> DeleteAsync(int id);
    Task<IDataResult<CategoryDto>> GetAsync(int id);
    Task<IDataResult<IEnumerable<CategoryViewModel>>> GetListAsync(Language? lang = null);
}
