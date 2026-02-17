using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace KPI.Pulse.UI.ViewModels
{
    public class IndexViewModel: ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public IEnumerable<PlatformItem> PlatformItems { get; init; }
        public IEnumerable<NavItem> NavItems { get; init; }
        public IEnumerable<TechItem> TechItems { get; init; }
        public int TechColumns { get; init; }

        public ReactiveCommand<NavItem, Unit> NavigateToNavItemCommand { get; }

        public IndexViewModel(IScreen screen)
        {
            HostScreen = screen;
            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            PlatformItems = uiService.GetPlatforms();
            NavItems = uiService.GetNavItems(HostScreen);
            TechItems = uiService.GetTechItems();
            TechColumns = Math.Min(TechItems.Count(), 4);

            NavigateToNavItemCommand = ReactiveCommand.Create<NavItem>(item =>
            {
                if (HostScreen is IInteractionViewModel interactionVm)
                {
                    interactionVm.NavigateToNavItem.Handle(item.Id).Subscribe();
                }
            });
        }
    }
}
