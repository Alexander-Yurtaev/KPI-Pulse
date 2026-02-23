using System.ComponentModel.DataAnnotations;

namespace KPI.Pulse.UI.Models.Enums
{
    public enum TableDataStatus
    {
        [Display(Name = "В процессе")]
        InProcess,

        [Display(Name = "Выполнено")]
        Ready
    }
}
