using Avalonia.Controls;
using KPI.Pulse.UI.ViewModels;
using KPI.Pulse.UI.ViewModels.Tree;
using ReactiveUI.Avalonia;

namespace KPI.Pulse.UI.Views;

public partial class AnalyticsView : ReactiveUserControl<AnalyticsViewModel>
{
    public AnalyticsView()
    {
        InitializeComponent();
    }

    private void TreeView_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (!(e.AddedItems?.Count > 0) || sender is not TreeView tree) return;

        var selected = e.AddedItems[0];
        if (selected is Leaf leaf)
        {
            tree.SelectedItem = leaf.Parent;
        }
    }
}