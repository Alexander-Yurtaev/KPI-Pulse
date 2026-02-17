using System.Collections.Generic;
using KPI.Pulse.UI.Models;
using ReactiveUI;

namespace KPI.Pulse.UI.Services;

public interface IUiService
{
    IEnumerable<PlatformItem> GetPlatforms();
    IEnumerable<NavItem> GetNavItems(IScreen hostScreen);
    IEnumerable<TechItem> GetTechItems();
    IEnumerable<DashboardIndicator> GetDashboardIndicators();
}