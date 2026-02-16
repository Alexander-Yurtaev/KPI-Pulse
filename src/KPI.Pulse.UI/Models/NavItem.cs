using System;
using System.Reactive;
using ReactiveUI;

namespace KPI.Pulse.UI.Models
{
    public class NavItem
    {
        public NavItem(string icon, string title, string subtitle, Func<IObservable<IRoutableViewModel>> execute)
        {
            Icon = icon; 
            Title = title;
            Subtitle = subtitle;
            GoTo = ReactiveCommand.CreateFromObservable(execute);
        }

        public string Icon { get; init; }
        public string Title { get; init; }
        public string Subtitle { get; init; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoTo { get; set; }
    }
}
