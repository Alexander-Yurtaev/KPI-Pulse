using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using KPI.Pulse.UI.ViewModels.Tree;

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

        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }
        
        public Node SelectedNode { get; set; }

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

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(navItem =>
            {
                if (HostScreen is IInteractionViewModel interactionVm)
                {
                    interactionVm.NavigateToNavItem.Handle(navItem.Id).Subscribe();
                }
            });
        }
    }
}
