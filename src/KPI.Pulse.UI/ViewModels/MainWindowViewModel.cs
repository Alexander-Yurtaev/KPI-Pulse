using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace KPI.Pulse.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen, IInteractionViewModel
    {
        public RoutingState Router { get; } = new RoutingState();
        public Interaction<int, Unit> NavigateToNavItem { get; } = new();

        public ObservableCollection<NavItem> NavItems { get; }

        private NavItem? _selectedNavItem;
        public NavItem? SelectedNavItem
        {
            get => _selectedNavItem;
            set => this.RaiseAndSetIfChanged(ref _selectedNavItem, value);
        }

        private bool _headIsVisible;
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
            NavItems = new ObservableCollection<NavItem>(uiService.GetNavItems(this));

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

            NavigateToNavItem.RegisterHandler(async interaction =>
            {
                var navItem = NavItems.FirstOrDefault(i => i.Id == interaction.Input);

                if (navItem?.IsActive == true)
                {
                    SelectedNavItem = navItem;
                }
                else
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard("Внимание", "Раздел в разработке!", ButtonEnum.Ok, Icon.Info);
                    await box.ShowWindowAsync();

                    SelectedNavItem = null;
                }

                interaction.SetOutput(Unit.Default);
            });

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(item =>
            {
                NavigateToNavItem.Handle(item.Id).Subscribe();
            });
        }
    }
}