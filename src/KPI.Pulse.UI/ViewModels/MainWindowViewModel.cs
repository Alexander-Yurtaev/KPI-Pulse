using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new IndexViewModel(this));
        }

        public RoutingState Router { get; } = new RoutingState();
    }
}