namespace KPI.Pulse.UI.Models
{
    public class NavItem
    {
        public NavItem(string icon, string title, string subtitle)
        {
            Icon = icon; 
            Title = title;
            Subtitle = subtitle;
        }

        public string Icon { get; init; }
        public string Title { get; init; }
        public string Subtitle { get; init; }
    }
}
