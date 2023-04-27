using Application.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Application.Utilities.Helpers;

public class Image
{
    public static async Task<IDataResult<string>> SaveImageToFile(IFormFile file, string webRootPath)
    {
        string fileName;
        try
        {
            fileName = "image-" + Guid.NewGuid().ToString().Substring(0, 12) + "." + file.ContentType.Split('/')[1];
            var uploadPath = Path.Combine(webRootPath, "images", fileName);

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

    public static void Delete(string fileName)
    {
        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + fileName;

        if (File.Exists(path))
            File.Delete(path);
    }


    public static IDataResult<byte[]> GetByteImage(string fileName)
    {

        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + fileName;

        if (!File.Exists(path))
            return new ErrorDataResult<byte[]>("Görsel Bulunamadı.");

        var image = File.ReadAllBytes(path);
        return new SuccessDataResult<byte[]>(image);
    }
}
