namespace Namaa.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Namaa.Domain.Common.Results;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file,string folderName,CancellationToken ct=default);
}