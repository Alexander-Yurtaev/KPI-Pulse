using KPI.Pulse.UI.ViewModels;
using ReactiveUI.Avalonia;

namespace KPI.Pulse.UI.Views;

public partial class IndexView : ReactiveUserControl<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();
    }
}