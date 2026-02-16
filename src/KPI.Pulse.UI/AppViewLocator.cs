using System;
using KPI.Pulse.UI.ViewModels;
using KPI.Pulse.UI.Views;
using ReactiveUI;

namespace KPI.Pulse.UI
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T? viewModel, string? contract = null)
        {
            return viewModel switch
            {
                IndexViewModel context => new IndexView { DataContext = context },
                AnalyticsViewModel context => new AnalyticsView { DataContext = context },
                DashboardViewModel context => new DashboardView { DataContext = context },
                SettingsViewModel context => new SettingsView { DataContext = context },
                _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
            };
        }
    }
}
