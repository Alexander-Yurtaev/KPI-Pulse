namespace KPI.Pulse.UI.Models
{
    public class PlatformItem
    {
        public PlatformItem(string icon, string title)
        {
            Icon = icon; 
            Title = title;
        }

        public string Icon { get; init; }
        public string Title { get; init; }
    }
}
