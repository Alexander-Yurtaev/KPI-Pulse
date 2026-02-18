using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive;

namespace KPI.Pulse.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        public NavItem RootNavItem { get; set; }
        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;

            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            var navItems = uiService.GetNavItems(HostScreen);
            RootNavItem = navItems.First();

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(_ =>
            {
                if (HostScreen is IInteractionViewModel interactionVm)
                {
                    interactionVm.NavigateToNavItem.Handle(RootNavItem.Id).Subscribe();
                }
            });
        }
}
}
