namespace WebLibCs.Web.ViewModels.ActionButtons;

public class ActionButtonsViewModel
{
    public int Id { get; set; }
    public string? Controller { get; set; }
    public string? ItemTitle { get; set; }
    public bool ShowEdit { get; set; } = true;
    public bool ShowDetails { get; set; } = true;
    public bool ShowDelete { get; set; } = true;
}