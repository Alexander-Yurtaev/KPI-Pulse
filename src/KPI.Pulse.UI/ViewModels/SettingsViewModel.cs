using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace KPI.Pulse.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        private readonly ObservableCollection<Kpi> _kpis;
        public ObservableCollection<Kpi> Kpis => _kpis;

        private readonly ObservableCollection<KpiViewModel> _savedKpiVms;
        public ObservableCollection<KpiViewModel> SavedKpiVms => _savedKpiVms;

        private readonly NavItem _rootNavItem;
        public NavItem RootNavItem
        {
            get => _rootNavItem;
            init => this.RaiseAndSetIfChanged(ref _rootNavItem, value);
        }

        private Kpi? _selectedKpi;
        public Kpi? SelectedKpi
        {
            get => _selectedKpi;
            set => this.RaiseAndSetIfChanged(ref _selectedKpi, value);
        }

        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }
        public ReactiveCommand<Kpi, Unit> AddKpiCommand { get; }

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            var savedKpis = uiService.GetSavedKpis().ToList();
            var savedKpiIds = savedKpis.Select(k => k.Id).ToArray();
            _savedKpiVms = new ObservableCollection<KpiViewModel>(savedKpis.Select(k => new KpiViewModel(k)));

            var kpis = uiService.GetKpis();
            kpis = kpis.Where(k => !savedKpiIds.Contains(k.Id)).ToArray();
            _kpis = new ObservableCollection<Kpi>(kpis);
            
            var navItems = uiService.GetNavItems(HostScreen);
            _rootNavItem = navItems.First();

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(navItem =>
            {
                if (HostScreen is IInteractionViewModel interactionVm)
                {
                    interactionVm.NavigateToNavItem.Handle(navItem.Id).Subscribe();
                }
            });

            var canAddKpiCommand = this.WhenAnyValue(vm => vm.SelectedKpi,
                (Kpi? selected) => selected != null);
            AddKpiCommand = ReactiveCommand.Create<Kpi>(_ =>
            {
                if (SelectedKpi is null) return;
                if (_savedKpiVms.Any(si => si.Base.Id == SelectedKpi.Id)) return;

                SavedKpiVms.Add(new KpiViewModel(SelectedKpi));
                var added = Kpis.FirstOrDefault(k => k.Id == SelectedKpi.Id);
                if (added is not null)
                {
                    Kpis.Remove(added);
                }

            }, canAddKpiCommand);
        }
    }
}