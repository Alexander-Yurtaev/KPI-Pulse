using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using KPI.Pulse.UI.ViewModels.Grid;
using KPI.Pulse.UI.ViewModels.Tree;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace KPI.Pulse.UI.ViewModels
{
    public class AnalyticsViewModel : ViewModelBase, IRoutableViewModel, IActivatableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        public ViewModelActivator Activator { get; }

        private readonly NavItem _rootNavItem;
        public NavItem RootNavItem
        {
            get => _rootNavItem;
            init => this.RaiseAndSetIfChanged(ref _rootNavItem, value);
        }

        private readonly ObservableCollection<Node> _treeItems;
        public ObservableCollection<Node> TreeItems
        {
            get => _treeItems;
            init => this.RaiseAndSetIfChanged(ref _treeItems, value);
        }

        private BaseTreeItem? _selectedNode;
        public BaseTreeItem? SelectedNode
        {
            get => _selectedNode;
            set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
        }

        public ObservableCollection<StackedColumnSeries<double>> Series => SelectedNode is Node node ? node.Series : [];

        public ObservableCollection<Axis> XAxes => SelectedNode is Node node ? node.XAxes : [];

        public ObservableCollection<Axis> YAxes => SelectedNode is Node node ? node.YAxes : [];

        private ObservableCollection<TableDataItem> _tableData;

        public ObservableCollection<TableDataItem> TableData
        {
            get => _tableData;
            set => this.RaiseAndSetIfChanged(ref _tableData, value);
        }

        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }

        public AnalyticsViewModel(IScreen screen)
        {
            HostScreen = screen;
            Activator = new ViewModelActivator();

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            var navItems = uiService.GetNavItems(HostScreen);
            _rootNavItem = navItems.First();

            var treeItems = uiService.KpiTreeItems();
            _treeItems = new ObservableCollection<Node>(treeItems);

            var tableData = uiService.GetTableData();
            _tableData = new ObservableCollection<TableDataItem>(tableData);

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(navItem =>
            {
                if (HostScreen is IInteractionViewModel interactionVm)
                {
                    interactionVm.NavigateToNavItem.Handle(navItem.Id).Subscribe();
                }
            });

            this.WhenAnyValue(x => x.SelectedNode)
                .Subscribe(async void (selected) =>
                {
                    this.RaisePropertyChanged(nameof(Series));
                    await Task.Delay(100); // fix issue when chart is not shown
                    this.RaisePropertyChanged(nameof(XAxes));
                    this.RaisePropertyChanged(nameof(YAxes));
                });
        }
    }
}
