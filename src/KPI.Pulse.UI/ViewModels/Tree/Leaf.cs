using Avalonia.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace KPI.Pulse.UI.ViewModels.Tree
{
    public class Leaf : BaseTreeItem
    {
        public Leaf(string icon, string title, string value) : base(icon, title)
        {
            Value = value;
        }

        public Node Parent { get; set; }
        public string Value { get; init; }
        public StackedColumnSeries<double> Series { get; init; }
    }
}
