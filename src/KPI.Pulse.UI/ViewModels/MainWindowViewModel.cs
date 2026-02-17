using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace KPI.Pulse.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen, IInteractionViewModel
    {
        private readonly ObservableCollection<NavItem> _navItems;
        private NavItem? _selectedNavItem;
        private bool _headIsVisible;

        public IEnumerable<NavItem> NavItems => _navItems;
        public RoutingState Router { get; } = new RoutingState();
        public Interaction<int, Unit> NavigateToNavItem { get; } = new();

        public NavItem? SelectedNavItem
        {
            get => _selectedNavItem;
            set => this.RaiseAndSetIfChanged(ref _selectedNavItem, value);
        }

        public bool HeadIsVisible
        {
            get => _headIsVisible;
            set => this.RaiseAndSetIfChanged(ref _headIsVisible, value);
        }

        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }

        public MainWindowViewModel()
        {
            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));
            _navItems = new ObservableCollection<NavItem>(uiService.GetNavItems(this));

            this.WhenAnyValue(x => x.SelectedNavItem)
                .Subscribe(selectedItem =>
                {
                    if (selectedItem is null)
                    {
                        HeadIsVisible = false;
                        Router.Navigate.Execute(new IndexViewModel(this));
                    }
                    else
                    {
                        HeadIsVisible = true;
                        selectedItem.GoTo.Execute();
                    }
                });

            NavigateToNavItem.RegisterHandler(interaction =>
            {
                SelectedNavItem = _navItems.FirstOrDefault(i => i.Id == interaction.Input);
                interaction.SetOutput(Unit.Default);
            });

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(item =>
            {
                NavigateToNavItem.Handle(item.Id).Subscribe();
            });
        }
    }
}