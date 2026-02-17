using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KPI.Pulse.UI.ViewModels
{
    public class DashboardViewModel: ViewModelBase, IRoutableViewModel
    {
        private readonly ObservableCollection<DashboardIndicator> _indicators;

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public IEnumerable<DashboardIndicator> Indicators => _indicators;

        public ChartViewModel Chart { get; set; }

        public DashboardViewModel(IScreen screen)
        {
            HostScreen = screen;

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            _indicators = new ObservableCollection<DashboardIndicator>(uiService.GetDashboardIndicators());
            Chart = new ChartViewModel();
        }
    }
}
