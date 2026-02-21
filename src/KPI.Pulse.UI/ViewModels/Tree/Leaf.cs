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
        }

        public Node Parent { get; set; }
        public string Value => Series.Values?.Sum().ToString(CultureInfo.InvariantCulture) ?? "0";
        public StackedColumnSeries<double> Series { get; init; }
    }
}
