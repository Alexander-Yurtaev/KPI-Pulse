using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using KPI.Pulse.UI.Models.Enums;
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
            yield return new NavItem(1, "📊", "Дашборд", "Главный экран с KPI-карточками и графиками", true, () => hostScreen.Router.Navigate.Execute(new DashboardViewModel(hostScreen)));
            yield return new NavItem(2, "📈", "Аналитика", "Детальный просмотр данных и история изменений", true, () => hostScreen.Router.Navigate.Execute(new AnalyticsViewModel(hostScreen)));
            yield return new NavItem(3, "⚙️", "Настройки", "Конфигурация мониторов и пороговых значений", true, () => hostScreen.Router.Navigate.Execute(new SettingsViewModel(hostScreen)));
        }

        public IEnumerable<TechItem> GetTechItems()
        {
            yield return new TechItem(".NET 8");
            yield return new TechItem("Avalonia UI 11");
            yield return new TechItem("MVVM паттерн");
            yield return new TechItem("JSON для хранения настроек");
        }

        public IEnumerable<Kpi> GetKpis()
        {
            yield return new Kpi(1, "Выручка", "Общая выручка компании за период", "💰", "2.4M ₽", 
                TrendStatus.Success, "▲", "+12.5 %", "к прошлому периоду",
                "Порог:", "▼", "1.5M ₽");

            yield return new Kpi(2, "Прибыль", "Чистая операционная прибыль", "📈", "845K ₽",
                TrendStatus.Danger, "▼", "-3.2 %", "к прошлому периоду",
                "", "⚠️", "Ниже порога(1M ₽)");

            yield return new Kpi(3, "Клиенты", "Количество новых клиентов", "👥", "1,284",
                TrendStatus.Success, "▲", "+8.3 %", "новых",
                "Цель:", "", "1500");

            yield return new Kpi(4, "Конверсия", "Процент конверсии в продажу", "🔄", "15.8 %",
                TrendStatus.Warning, "▲", "+1.2 %", "к прошлому",
                "Целевой диапазон:", "⚡", "14 - 18%");
        }

        public IEnumerable<Alert> GetAlerts()
        {
            yield return new Alert("⚠️", "Прибыль ниже порога", AlertStatus.Danger,"Текущее значение: 845K ₽ (порог: 1M ₽)");
            yield return new Alert("⚡", "Конверсия приближается к верхней границе", AlertStatus.Warning, "15.8% (диапазон: 14-18%)");
        }

        public IEnumerable<Goal> GetGoals()
        {
            yield return new Goal("💰", "Выручка", 2_400_000, 3_000_000, "2.4M / 3M");
            yield return new Goal("👥", "Клиенты", 1_284, 1_500, "1 284 / 1 500");
            yield return new Goal("🔄", "Конверсия", 15.8, 18, "15.8% / 18%");
        }

        public IEnumerable<Kpi> GetSavedKpis()
        {
            return GetKpis().Take(1);
        }
    }
}