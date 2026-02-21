using DynamicData;
using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Threading.Tasks;

namespace KPI.Pulse.UI.ViewModels
{
    public class DashboardViewModel: ViewModelBase, IRoutableViewModel, IActivatableViewModel
    {
        private readonly ObservableCollection<Kpi> _kpis;
        private readonly ObservableCollection<Alert> _alerts;
        private readonly ObservableCollection<Goal> _goals;

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; } = null!;
        public ViewModelActivator Activator { get; }

        public IEnumerable<Kpi> Kpis => _kpis;

        public ChartViewModel Chart { get; set; }
        public int AlertsCount => Alerts?.Count() ?? 0;
        public IEnumerable<Alert> Alerts => _alerts;
        public IEnumerable<Goal> Goals => _goals;

        public DashboardViewModel()
        {
            Activator = new ViewModelActivator();

            Chart = new ChartViewModel();

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            var kps = uiService.GetKpis();

            _kpis = new ObservableCollection<Kpi>();
            _alerts = new ObservableCollection<Alert>(uiService.GetAlerts());
            _goals = new ObservableCollection<Goal>(uiService.GetGoals());

            this.WhenActivated(disposables =>
            {
                // Загружаем данные при активации
                LoadDataAsync(uiService)
                    .ToObservable()
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        public DashboardViewModel(IScreen screen) : this()
        {
            HostScreen = screen;
        }

        private async System.Threading.Tasks.Task LoadDataAsync(IUiService uiService)
        {
            var kpis = uiService.GetKpis();

            var settingService = Locator.Current.GetService<ISettingService>() ??
                                 throw new InvalidOperationException(nameof(ISettingService));
            var settings = await settingService.LoadAsync();

            var savedKpiIds = settings.KpiConfig?.Ids ?? [];
            var savedKpis = kpis.Where(k => savedKpiIds.Contains(k.Id)).ToArray();

            _kpis.Clear();
            _kpis.AddRange(savedKpis);
        }
    }
}
