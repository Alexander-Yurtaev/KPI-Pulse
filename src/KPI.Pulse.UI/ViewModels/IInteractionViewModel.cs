using System.Reactive;
using ReactiveUI;

namespace KPI.Pulse.UI.ViewModels;

public interface IInteractionViewModel
{
    Interaction<int, Unit> NavigateToNavItem { get; }
}