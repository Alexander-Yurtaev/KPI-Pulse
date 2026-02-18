using KPI.Pulse.UI.Models.Enums;

namespace KPI.Pulse.UI.Models
{
    public class DashboardIndicator
    {
        public DashboardIndicator(string title, string icon, string value,
            TrendStatus trendStatus, string trendIcon, string trendValue, string trendDescription, string thresholdTitle, 
            string thresholdIcon, string thresholdValue)
        {
            Title = title;
            Icon = icon;
            Value = value;
            TrendStatus = trendStatus;
            TrendIcon = trendIcon;
            TrendValue = trendValue;
            TrendDescription = trendDescription;
            ThresholdTitle = thresholdTitle;
            ThresholdIcon = thresholdIcon;
            ThresholdValue = thresholdValue;
        }

        public string Title { get; init; }
        public string Icon { get; init; }
        public string Value { get; init; }
        public TrendStatus TrendStatus { get; init; }
        public string TrendIcon { get; init; }
        public string TrendValue { get; init; }
        public string TrendDescription { get; init; }
        public string ThresholdTitle { get; init; }
        public string ThresholdIcon { get; init; }
        public string ThresholdValue { get; init; }
    }
}
