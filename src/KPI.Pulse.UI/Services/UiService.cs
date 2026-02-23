using KPI.Pulse.UI.Models;
using KPI.Pulse.UI.Models.Enums;
using KPI.Pulse.UI.ViewModels;
using KPI.Pulse.UI.ViewModels.Grid;
using KPI.Pulse.UI.ViewModels.Tree;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPI.Pulse.UI.Services
{
    public class UiService : IUiService
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
            yield return new Alert("⚠️", "Прибыль ниже порога", AlertStatus.Danger, "Текущее значение: 845K ₽ (порог: 1M ₽)");
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

        public IEnumerable<Node> KpiTreeItems()
        {
            var items = new List<Node>();

            var rootItem = new Node("📊", "Все показатели");

            var revenue = new Node("💰", "Выручка") { Parent = rootItem };
            var profit = new Node("📈", "Прибыль") { Parent = rootItem };
            var clients = new Node("👥", "Клиенты") { Parent = rootItem };
            var conversion = new Node("🔄", "Конверсия") { Parent = rootItem };

            rootItem.Children.Add(revenue);
            rootItem.Children.Add(profit);
            rootItem.Children.Add(clients);
            rootItem.Children.Add(conversion);

            revenue.Children.Add(new Leaf("🇷🇺", "РФ") { Parent = revenue, Series = RevenueStackedColumnSeries("РФ", new SKColor(0x66, 0x7e, 0xea)) });
            revenue.Children.Add(new Leaf("🇰🇿", "СНГ") { Parent = revenue, Series = RevenueStackedColumnSeries("СНГ", new SKColor(0xf6, 0xad, 0x55)) });
            revenue.Children.Add(new Leaf("🌍", "Другое") { Parent = revenue, Series = RevenueStackedColumnSeries("Другое", new SKColor(0x48, 0xbb, 0x78)) });

            profit.Children.Add(new Leaf("🇷🇺", "РФ") { Parent = profit, Series = GetProfitStackedColumnSeries("РФ", new SKColor(0x66, 0x7e, 0xea)) });
            profit.Children.Add(new Leaf("🇰🇿", "СНГ") { Parent = profit, Series = GetProfitStackedColumnSeries("СНГ", new SKColor(0xf6, 0xad, 0x55)) });
            profit.Children.Add(new Leaf("🌍", "Другое") { Parent = profit, Series = GetProfitStackedColumnSeries("Другое", new SKColor(0x48, 0xbb, 0x78)) });

            clients.Children.Add(new Leaf("🇷🇺", "РФ") { Parent = clients, Series = GetClientsStackedColumnSeries("РФ", new SKColor(0x66, 0x7e, 0xea)) });
            clients.Children.Add(new Leaf("🇰🇿", "СНГ") { Parent = clients, Series = GetClientsStackedColumnSeries("СНГ", new SKColor(0xf6, 0xad, 0x55)) });
            clients.Children.Add(new Leaf("🌍", "Другое") { Parent = clients, Series = GetClientsStackedColumnSeries("Другое", new SKColor(0x48, 0xbb, 0x78)) });

            conversion.Children.Add(new Leaf("🇷🇺", "РФ") { Parent = conversion, Series = GetConversionStackedColumnSeries("РФ", new SKColor(0x66, 0x7e, 0xea)) });
            conversion.Children.Add(new Leaf("🇰🇿", "СНГ") { Parent = conversion, Series = GetConversionStackedColumnSeries("СНГ", new SKColor(0xf6, 0xad, 0x55)) });
            conversion.Children.Add(new Leaf("🌍", "Другое") { Parent = conversion, Series = GetConversionStackedColumnSeries("Другое", new SKColor(0x48, 0xbb, 0x78)) });

            foreach (var node in rootItem.Children.OfType<Node>())
            {
                node.InitSeries();
            }

            rootItem.InitSeries();

            items.Add(rootItem);
            return items;
        }

        public ColumnSeries<double>[] CreateColumnSeries()
        {
            var result = new List<ColumnSeries<double>>
            {
                RevenueColumnSeries("Выручка", new SKColor(0x66, 0x7e, 0xea)),
                GetClientsColumnSeries("Клиенты", new SKColor(0xf6, 0xad, 0x55)),
                GetConversionColumnSeries("Прибыль", new SKColor(0x48, 0xbb, 0x78))
            };

            return result.ToArray();
        }

        //< th > Период</ th>
        //< th > Выручка</ th>
        //< th > Прибыль</ th>
        //< th > Клиенты</ th>
        //< th > Конверсия</ th>
        //< th > Маржа</ th>
        //< th > Статус</ th>

        public IEnumerable<TableDataItem> GetTableData()
        {
            yield return new TableDataItem("Январь 2024", "1,245,000 ₽", "345,000 ₽", "1,245", "15.2%", "27.7%", TableDataStatus.Ready);
            yield return new TableDataItem("Февраль 2024", "1,389,000 ₽", "389,000 ₽", "1,289", "16.1%", "28.0%", TableDataStatus.Ready);
            yield return new TableDataItem("Март 2024", "1,567,000 ₽", "412,000 ₽", "1,334", "-15.8%", "26.3%", TableDataStatus.InProcess);
            yield return new TableDataItem("Апрель 2024", "1,623,000 ₽", "435,000 ₽", "1,378", "16.3%", "26.8%", TableDataStatus.Ready);
            yield return new TableDataItem("Май 2024", "1,712,000 ₽", "458,000 ₽", "1,422", "16.7%", "26.8%", TableDataStatus.Ready);
        }

        private ColumnSeries<double> RevenueColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new ColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#667eea
            };
        }

        private ColumnSeries<double> GetClientsColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new ColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#f6ad55
            };
        }

        private ColumnSeries<double> GetConversionColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new ColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#48bb78
            };
        }

        private StackedColumnSeries<double> RevenueStackedColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new StackedColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#667eea
            };
        }

        private StackedColumnSeries<double> GetProfitStackedColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new StackedColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#667eea
            };
        }

        private StackedColumnSeries<double> GetClientsStackedColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new StackedColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#f6ad55
            };
        }

        private StackedColumnSeries<double> GetConversionStackedColumnSeries(string name, SKColor color)
        {
            var random = new Random();

            return new StackedColumnSeries<double>
            {
                Name = name,
                Values = Enumerable.Range(1, 7).Select(_ => Math.Ceiling(100 * random.NextDouble())).ToArray(),
                Fill = new SolidColorPaint(color) //#48bb78
            };
        }
    }
}