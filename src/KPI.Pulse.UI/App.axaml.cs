using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KPI.Pulse.UI.ViewModels;
using KPI.Pulse.UI.Views;
using ReactiveUI;
using Splat;

namespace KPI.Pulse.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Locator.CurrentMutable.Register<IViewFor<IndexViewModel>>(() => new IndexView());
                Locator.CurrentMutable.Register<IViewFor<AnalyticsViewModel>>(() => new AnalyticsView());
                Locator.CurrentMutable.Register<IViewFor<DashboardViewModel>>(() => new DashboardView());
                Locator.CurrentMutable.Register<IViewFor<SettingsViewModel>>(() => new SettingsView());

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}