using System.Collections.Generic;

namespace KPI.Pulse.UI.ViewModels.Tree;

public class Node : BaseTreeItem
{
    public Node(string icon, string title) : base(icon, title)
    {
        Children = [];
    }

    public List<BaseTreeItem> Children { get; set; }
}