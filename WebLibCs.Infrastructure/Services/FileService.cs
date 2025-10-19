using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebLibCs.Core.Interfaces.Services;

namespace WebLibCs.Infrastructure.Services;

public record FileService(IWebHostEnvironment Environment) : IFileService
{
    private readonly IWebHostEnvironment _environment = Environment;

    public async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty", nameof(file));

        var uploadsFolder = Path.Combine(_environment.WebRootPath, folder);
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        var fileStream = new FileStream(filePath, FileMode.Create);
        await using (fileStream.ConfigureAwait(false))
        {
            await file.CopyToAsync(fileStream).ConfigureAwait(false);

            return Path.Combine(folder, uniqueFileName).Replace("\\", "/", StringComparison.Ordinal);
        }
    }

    public Task<bool> DeleteFileAsync(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return Task.FromResult(true);

        var fullPath = GetPhysicalPath(filePath);
        if (!File.Exists(fullPath))
            return Task.FromResult(true);

        try
        {
            File.Delete(fullPath);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public bool FileExists(string filePath)
    {
        var fullPath = GetPhysicalPath(filePath);
        return File.Exists(fullPath);
    }

    public string GetPhysicalPath(string relativePath)
    {
        return Path.Combine(_environment.WebRootPath, relativePath);
    }
}