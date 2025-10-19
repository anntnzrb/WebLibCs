namespace WebLibCs.Core.Common;

public static class Constants
{
    internal static class ImageSettings
    {
        public const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        public const string DefaultFolder = "imagenes";
    }

    internal static class Pagination
    {
        public const int DefaultPageSize = 5;
        public const int MaxPageSize = 50;
    }

    internal static class Validation
    {
        public const int MaxAutorNameLength = 100;
        public const int MaxLibroTitleLength = 200;
        public const int MaxImagePathLength = 500;
    }

    // Public constants for external use
    public const long MaxFileSize = ImageSettings.MaxFileSize;
    public static readonly string[] AllowedExtensions = ImageSettings.AllowedExtensions;
    public const string DefaultFolder = ImageSettings.DefaultFolder;
    public const int DefaultPageSize = Pagination.DefaultPageSize;
    public const int MaxPageSize = Pagination.MaxPageSize;
    public const int MaxAutorNameLength = Validation.MaxAutorNameLength;
    public const int MaxLibroTitleLength = Validation.MaxLibroTitleLength;
    public const int MaxImagePathLength = Validation.MaxImagePathLength;
}