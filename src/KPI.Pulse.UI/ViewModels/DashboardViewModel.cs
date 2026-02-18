using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KPI.Pulse.UI.ViewModels
{
    public class DashboardViewModel: ViewModelBase, IRoutableViewModel
    {
        private readonly ObservableCollection<DashboardIndicator> _indicators;
        private readonly ObservableCollection<Alert> _alerts;

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public IEnumerable<DashboardIndicator> Indicators => _indicators;

        public ChartViewModel Chart { get; set; }
        public int AlertsCount => Alerts?.Count() ?? 0;
        public IEnumerable<Alert> Alerts => _alerts;

        public DashboardViewModel()
        {
        }

        public DashboardViewModel(IScreen screen) : this()
        {
            HostScreen = screen;

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            _indicators = new ObservableCollection<DashboardIndicator>(uiService.GetDashboardIndicators());
            _alerts = new ObservableCollection<Alert>(uiService.GetAlerts());
            Chart = new ChartViewModel();
        }
    }
}
