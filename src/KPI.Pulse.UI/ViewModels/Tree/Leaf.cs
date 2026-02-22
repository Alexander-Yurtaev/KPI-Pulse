using System.Globalization;
using System.Linq;
using Avalonia.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace KPI.Pulse.UI.ViewModels.Tree
{
    public class Leaf : BaseTreeItem
    {
        public Leaf(string icon, string title) : base(icon, title)
        {
            Series = new StackedColumnSeries<double>();
        }

        public double Value => Series.Values?.Sum() ?? 0;
        public StackedColumnSeries<double> Series { get; init; }
    }
}
