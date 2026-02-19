using DynamicData;
using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Models.Settings;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace KPI.Pulse.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IRoutableViewModel, IActivatableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public ViewModelActivator Activator { get; }

        private readonly ObservableCollection<Kpi> _comboboxKpis = new ObservableCollection<Kpi>();
        public ObservableCollection<Kpi> ComboboxKpis => _comboboxKpis;

        private readonly ObservableCollection<KpiViewModel> _savedKpiVms = new ObservableCollection<KpiViewModel>();
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
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<KpiViewModel, Unit> DeleteCommand { get; }

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;
            Activator = new ViewModelActivator();

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

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

            AddKpiCommand = ReactiveCommand.Create<Kpi>(kpi =>
            {
                if (kpi is null) return;
                if (_savedKpiVms.Any(si => si.Base.Id == kpi.Id)) return;

                SavedKpiVms.Add(new KpiViewModel(kpi));
                var added = ComboboxKpis.FirstOrDefault(k => k.Id == kpi.Id);
                if (added is not null)
                {
                    ComboboxKpis.Remove(added);
                }
                SelectedKpi = null; // Сбрасываем выделение после добавления
            }, canAddKpiCommand);

            SaveCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                try
                {
                    var settingService = Locator.Current.GetService<ISettingService>() ??
                                         throw new InvalidOperationException(nameof(ISettingService));
                    var settings = new Config
                    {
                        KpiConfig = new KpiConfig
                        {
                            Ids = _savedKpiVms.Select(k => k.Base.Id).ToArray()
                        }
                    };
                    await settingService.SaveAsync(settings);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            DeleteCommand = ReactiveCommand.Create<KpiViewModel>(kpiVm =>
            {
                if (kpiVm is null) return;
                if (_comboboxKpis.Any(si => si.Id == kpiVm.Base.Id)) return;

                ComboboxKpis.Add(kpiVm.Base);
                var deleted = SavedKpiVms.FirstOrDefault(k => k.Base.Id == kpiVm.Base.Id);
                if (deleted is not null)
                {
                    SavedKpiVms.Remove(deleted);
                }
                SelectedKpi = deleted?.Base;
            });

            this.WhenActivated(disposables =>
            {
                // Загружаем данные при активации
                LoadDataAsync(uiService)
                    .ToObservable()
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        private async Task LoadDataAsync(IUiService uiService)
        {
            try
            {
                var allKpis = uiService.GetKpis().ToArray();

                _comboboxKpis.Clear();
                _comboboxKpis.AddRange(allKpis);

                var settingService = Locator.Current.GetService<ISettingService>() ??
                                     throw new InvalidOperationException(nameof(ISettingService));
                var settings = await settingService.LoadAsync();

                var savedKpiIds = settings.KpiConfig?.Ids ?? [];
                var savedKpis = allKpis.Where(k => savedKpiIds.Contains(k.Id)).ToArray();

                _savedKpiVms.Clear();

                foreach (var kpi in savedKpis)
                {
                    // Добавляем в список возможных KPI
                    _savedKpiVms.Add(new KpiViewModel(kpi));

                    // Удаляем из списка доступных KPI
                    var comboboxKpi = _comboboxKpis.FirstOrDefault(k => k.Id == kpi.Id);
                    if (comboboxKpi != null)
                    {
                        _comboboxKpis.Remove(comboboxKpi);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}