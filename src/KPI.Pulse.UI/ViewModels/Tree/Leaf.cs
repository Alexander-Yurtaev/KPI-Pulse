using LiveChartsCore;

namespace KPI.Pulse.UI.ViewModels.Tree
{
    public class Leaf : BaseTreeItem
    {
        public Leaf(string icon, string title, string value) : base(icon, title)
        {
            Value = value;
        }

        
        public string Value { get; init; }
        public ISeries[] Series { get; init; }
    }
}
