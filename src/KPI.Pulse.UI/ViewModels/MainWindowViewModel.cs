using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using KPI.Pulse.UI.Models;
using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IEnumerable<PlatformItem> PlatformItems { get; init; }
        public IEnumerable<NavItem> NavItems { get; init; }
        public IEnumerable<TechItem> TechItems { get; init; }
        public int TechColumns { get; init; }

        public MainWindowViewModel()
        {
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
            yield return new NavItem("📊", "Дашборд", "Главный экран с KPI-карточками и графиками");
            yield return new NavItem("📈", "Аналитика", "Детальный просмотр данных и история изменений");
            yield return new NavItem("⚙️", "Настройки", "Конфигурация мониторов и пороговых значений");
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