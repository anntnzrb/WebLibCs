namespace WebLibCs.Web.ViewModels.Home;

public record ErrorViewModel(string? RequestId = null)
{
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}