using Microsoft.AspNetCore.Http;

namespace WebLibCs.Core.Interfaces.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folder);
    Task<bool> DeleteFileAsync(string? filePath);
    bool FileExists(string filePath);
    string GetPhysicalPath(string relativePath);
}