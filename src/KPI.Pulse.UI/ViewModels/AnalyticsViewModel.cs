using System;
using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels
{
    public class AnalyticsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public AnalyticsViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
