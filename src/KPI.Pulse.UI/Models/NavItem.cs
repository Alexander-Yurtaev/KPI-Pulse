using System;
using System.Reactive;
using ReactiveUI;

namespace KPI.Pulse.UI.Models
{
    public class NavItem
    {
        public NavItem(int id, string icon, string title, string subtitle, bool isActive, Func<IObservable<IRoutableViewModel>> execute)
        {
            Id = id;
            Icon = icon; 
            Title = title;
            Subtitle = subtitle;
            IsActive = isActive;
            GoTo = ReactiveCommand.CreateFromObservable(execute);
        }

        public int Id { get; init; }
        public string Icon { get; init; }
        public string Title { get; init; }
        public string Subtitle { get; init; }
        public bool IsActive { get; init; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoTo { get; set; }
    }
}
