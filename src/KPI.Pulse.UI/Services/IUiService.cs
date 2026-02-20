using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.ViewModels.Tree;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using System.Collections.Generic;

namespace KPI.Pulse.UI.Services;

public interface IUiService
{
    IEnumerable<PlatformItem> GetPlatforms();
    IEnumerable<NavItem> GetNavItems(IScreen hostScreen);
    IEnumerable<TechItem> GetTechItems();
    IEnumerable<Kpi> GetKpis();
    IEnumerable<Alert> GetAlerts();
    IEnumerable<Goal> GetGoals();
    IEnumerable<Kpi> GetSavedKpis();
    IEnumerable<Node> KpiTreeItems();
    ColumnSeries<double>[] CreateColumnSeries();
}