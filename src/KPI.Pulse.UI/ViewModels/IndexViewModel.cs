using KPI.Pulse.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

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

        public IndexViewModel(IScreen screen)
        {
            HostScreen = screen;

            PlatformItems = GetPlatforms();
            NavItems = GetNavItems();
            TechItems = GetTechItems();
            TechColumns = Math.Min(TechItems.Count(), 4);
        }

        #region Private Methhods

        private IEnumerable<PlatformItem> GetPlatforms()
        {
            yield return new PlatformItem("🖥️", "Windows 10/11");
            yield return new PlatformItem("🐧", "Linux (Ubuntu, Debian, Fedora)");
            yield return new PlatformItem("🍏", "macOS (Intel и Apple Silicon)");
        }

        private IEnumerable<NavItem> GetNavItems()
        {
            yield return new NavItem("📊", "Дашборд", "Главный экран с KPI-карточками и графиками", () => HostScreen.Router.Navigate.Execute(new DashboardViewModel(HostScreen)));
            yield return new NavItem("📈", "Аналитика", "Детальный просмотр данных и история изменений", () => HostScreen.Router.Navigate.Execute(new AnalyticsViewModel(HostScreen)));
            yield return new NavItem("⚙️", "Настройки", "Конфигурация мониторов и пороговых значений", () => HostScreen.Router.Navigate.Execute(new SettingsViewModel(HostScreen)));
        }

        private IEnumerable<TechItem> GetTechItems()
        {
            yield return new TechItem(".NET 8");
            yield return new TechItem("Avalonia UI 11");
            yield return new TechItem("MVVM паттерн");
            yield return new TechItem("JSON для хранения настроек");
        }

        #endregion Private Methhods
    }
}
