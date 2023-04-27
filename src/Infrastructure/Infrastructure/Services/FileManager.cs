using Application.Interfaces.Services;
using Application.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileManager : IFileService
{
    private readonly IHostingEnvironment environment;

    public FileManager(IHostingEnvironment environment)
    {
        this.environment = environment;
    }


    public async Task<IDataResult<string>> SaveImageToFile(IFormFile file)
    {
        string fileName;
        try
        {
            fileName = "image-" + Guid.NewGuid().ToString().Substring(0, 12) + "." + file.ContentType.Split('/')[1];
            var uploadPath = Path.Combine(environment.WebRootPath, "images", fileName);

            var fs = File.Create(uploadPath);
            await file.CopyToAsync(fs);
            await fs.FlushAsync();
        }
        catch (Exception e)
        {
            return new ErrorDataResult<string>(e.Message);
        }

        return new SuccessDataResult<string>(fileName, "");
    }

    public async Task<IResult> Delete(string fileName)
    {
        try
        {
            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + fileName;

            if (File.Exists(path))
                File.Delete(path);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<string>(e.Message);
        }

        return new SuccessDataResult<string>(fileName, "");

    }

}
