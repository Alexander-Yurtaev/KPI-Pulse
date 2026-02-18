using KPI.Pulse.UI.Models.Enums;

namespace KPI.Pulse.UI.Models
{
    public class Alert
    {
        public Alert(string icon, string title, AlertStatus alertStatus, string description)
        {
            Icon = icon;
            Title = title;
            AlertStatus = alertStatus;
            Description = description;
        }

        public string Icon { get; init; }
        public string Title { get; init; }
        public AlertStatus AlertStatus { get; set; }
        public string Description { get; init; }
    }
}
