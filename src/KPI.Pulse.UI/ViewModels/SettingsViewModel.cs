using System;
using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
