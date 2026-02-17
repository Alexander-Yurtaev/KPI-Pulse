using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.ViewModels;
using System.Collections.Generic;
using ReactiveUI;

namespace KPI.Pulse.UI.Services
{
    public class UiService: IUiService
    {
        public IEnumerable<PlatformItem> GetPlatforms()
        {
            yield return new PlatformItem("🖥️", "Windows 10/11");
            yield return new PlatformItem("🐧", "Linux (Ubuntu, Debian, Fedora)");
            yield return new PlatformItem("🍏", "macOS (Intel и Apple Silicon)");
        }

        public IEnumerable<NavItem> GetNavItems(IScreen hostScreen)
        {
            yield return new NavItem(1, "📊", "Дашборд", "Главный экран с KPI-карточками и графиками", () => hostScreen.Router.Navigate.Execute(new DashboardViewModel(hostScreen)));
            yield return new NavItem(2, "📈", "Аналитика", "Детальный просмотр данных и история изменений", () => hostScreen.Router.Navigate.Execute(new AnalyticsViewModel(hostScreen)));
            yield return new NavItem(3, "⚙️", "Настройки", "Конфигурация мониторов и пороговых значений", () => hostScreen.Router.Navigate.Execute(new SettingsViewModel(hostScreen)));
        }

        public IEnumerable<TechItem> GetTechItems()
        {
            yield return new TechItem(".NET 8");
            yield return new TechItem("Avalonia UI 11");
            yield return new TechItem("MVVM паттерн");
            yield return new TechItem("JSON для хранения настроек");
        }

        public IEnumerable<DashboardIndicator> GetDashboardIndicators()
        {
            yield return new DashboardIndicator("Выручка", "💰", "2.4M ₽", 
                true, "▲", "+12.5 %", "к прошлому периоду",
                "Порог:", "▼", "1.5M ₽");

            yield return new DashboardIndicator("Прибыль", "📈", "845K ₽",
                false, "▼", "-3.2 %", "к прошлому периоду",
                "", "⚠️", "Ниже порога(1M ₽)");

            yield return new DashboardIndicator("Клиенты", "👥", "1,284",
                true, "▲", "+8.3 %", "новых",
                "Цель:", "", "1500");

            yield return new DashboardIndicator("Конверсия", "🔄", "15.8 %",
                true, "▲", "+1.2 %", "к прошлому",
                "Целевой диапазон:", "⚡", "14 - 18%");
        }
    }
}