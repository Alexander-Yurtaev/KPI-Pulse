using System;
using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels
{
    public class DashboardViewModel: ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public DashboardViewModel()
        {
            
        }

        public DashboardViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
