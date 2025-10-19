using Microsoft.AspNetCore.Http;

namespace WebLibCs.Core.Interfaces.Services;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file, string folder);
    Task<bool> DeleteImageAsync(string? imagePath);
    Task<string?> UpdateImageAsync(IFormFile? newFile, string? oldPath, string folder);
    bool IsValidImage(IFormFile? file);
}