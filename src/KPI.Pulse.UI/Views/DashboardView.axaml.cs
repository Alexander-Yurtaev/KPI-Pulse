using KPI.Pulse.UI.ViewModels;
using ReactiveUI.Avalonia;

namespace KPI.Pulse.UI.Views;

public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
{
    public DashboardView()
    {
        InitializeComponent();
    }
}