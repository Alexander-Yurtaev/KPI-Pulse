namespace KPI.Pulse.UI.ViewModels.Tree;

public abstract class BaseTreeItem : ViewModelBase
{
    protected BaseTreeItem(string icon, string title)
    {
        Icon = icon;
        Title = title;
    }

    public string Icon { get; set; }
    public string Title { get; set; }
    public Node? Parent { get; set; }
}