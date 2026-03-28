using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Namaa.Application.Common.Interfaces;
using Namaa.Infrastructure.Settings;

namespace Namaa.Infrastructure.Services;

// public class CloudinaryFileService(IOptions<CloudinarySettings> options):IFileService
// {
   
//      private readonly Cloudinary _cloudinary = new Cloudinary(new Account(
//         options.Value.CloudName, 
//         options.Value.ApiKey, 
//         options.Value.ApiSecret));
//      public async Task<string> UploadFileAsync(IFormFile file, string folderName, CancellationToken ct = default)
//     {
//         using var stream=file.OpenReadStream();
//         var extension=Path.GetExtension(file.FileName).ToLower();
//         var cloudFolder=$"Namaa/{folderName}";
//         if(extension == ".pdf")
//         {
//          var uploadParams=new RawUploadParams()
//          {
//              File=new FileDescription(file.FileName,stream),
//              Folder=cloudFolder
//          };
//          var uploadResult=await _cloudinary.UploadAsync(uploadParams,cancellationToken:ct);
//           return uploadResult.SecureUrl.ToString();
//         }
//         else
//         {
//             var uploadParams=new ImageUploadParams
//             {
//                 File=new FileDescription(file.FileName,stream),
//                 Folder=cloudFolder
//             };
//             var uploadResult=await _cloudinary.UploadAsync(uploadParams,ct);
//               return uploadResult.SecureUrl.ToString();

//         }

//     }
   
// }
public class CloudinaryFileService(IOptions<CloudinarySettings> options) : IFileService
{
    private readonly Cloudinary _cloudinary = new Cloudinary(new Account(
        options.Value.CloudName,
        options.Value.ApiKey,
        options.Value.ApiSecret));

    public async Task<string> UploadFileAsync(IFormFile file, string folderName, CancellationToken ct = default)
    {
        using var stream = file.OpenReadStream();
        var extension = Path.GetExtension(file.FileName).ToLower();
        var cloudFolder = $"Namaa/{folderName}";

        try
        {
            if (extension == ".pdf")
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = cloudFolder
                };
                var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken:ct);
                
                if (result.Error != null)
                {
                    throw new Exception(result.Error.Message);
                }

                return result.SecureUrl.ToString();
            }
            else
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = cloudFolder
                };
                var result = await _cloudinary.UploadAsync(uploadParams, ct);

                if (result.Error != null)
                {
                    throw new Exception(result.Error.Message);
                }

                return result.SecureUrl.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Cloudinary upload failed: {ex.Message}", ex);
        }
    }
}