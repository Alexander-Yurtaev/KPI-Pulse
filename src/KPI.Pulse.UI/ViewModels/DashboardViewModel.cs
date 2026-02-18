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
        private readonly ObservableCollection<Kpi> _kpis;
        private readonly ObservableCollection<Alert> _alerts;
        private readonly ObservableCollection<Goal> _goals;

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public IEnumerable<Kpi> Kpis => _kpis;

        public ChartViewModel Chart { get; set; }
        public int AlertsCount => Alerts?.Count() ?? 0;
        public IEnumerable<Alert> Alerts => _alerts;
        public IEnumerable<Goal> Goals => _goals;

        public DashboardViewModel()
        {
            Chart = new ChartViewModel();

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            _kpis = new ObservableCollection<Kpi>(uiService.GetKpis());
            _alerts = new ObservableCollection<Alert>(uiService.GetAlerts());
            _goals = new ObservableCollection<Goal>(uiService.GetGoals());
        }

        public DashboardViewModel(IScreen screen) : this()
        {
            HostScreen = screen;
        }
    }
}
