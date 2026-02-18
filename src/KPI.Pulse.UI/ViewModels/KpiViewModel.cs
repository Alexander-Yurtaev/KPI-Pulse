using KPI.Pulse.UI.Models;

namespace KPI.Pulse.UI.ViewModels
{
    public class KpiViewModel : ViewModelBase
    {
        public KpiViewModel(Kpi @base)
        {
            Base = @base;
        }

        public Kpi Base { get; set; }
    }
}
