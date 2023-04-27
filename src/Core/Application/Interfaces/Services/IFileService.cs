using Application.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IFileService
{
    Task<IDataResult<string>> SaveImageToFile(IFormFile file);
    Task<IResult> Delete(string fileName);
}
