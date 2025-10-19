using Microsoft.AspNetCore.Http;
using WebLibCs.Core.Common;
using WebLibCs.Core.Exceptions;
using WebLibCs.Core.Interfaces.Services;

namespace WebLibCs.Infrastructure.Services;

public record ImageService(IFileService FileService) : IImageService
{
    private readonly IFileService _fileService = FileService;

    public async Task<string> SaveImageAsync(IFormFile file, string folder)
    {
        if (!IsValidImage(file))
        {
            throw new InvalidFileException("Invalid image file format or size", file?.FileName ?? "unknown", file?.Length);
        }

        return await _fileService.SaveFileAsync(file, folder).ConfigureAwait(false);
    }

    public async Task<bool> DeleteImageAsync(string? imagePath)
    {
        return await _fileService.DeleteFileAsync(imagePath).ConfigureAwait(false);
    }

    public async Task<string?> UpdateImageAsync(IFormFile? newFile, string? oldPath, string folder)
    {
        // If no new file, return the old path
        if (newFile == null || newFile.Length == 0)
            return oldPath;

        // Validate the new file
        if (!IsValidImage(newFile))
        {
            throw new InvalidFileException("Invalid image file format or size", newFile.FileName ?? "unknown", newFile.Length);
        }

        // Delete old image if it exists
        if (!string.IsNullOrEmpty(oldPath))
        {
            await DeleteImageAsync(oldPath).ConfigureAwait(false);
        }

        // Save new image
        return await SaveImageAsync(newFile, folder).ConfigureAwait(false);
    }

    public bool IsValidImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return false;

        // Check file size
        if (file.Length > Constants.MaxFileSize)
            return false;

        // Check file extension
        var extension = Path.GetExtension(file.FileName).ToUpperInvariant();
        return Constants.AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }
}