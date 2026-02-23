using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using KPI.Pulse.UI.Models.Enums;

namespace KPI.Pulse.UI.ViewModels.Grid
{
    public class TableDataItem
    {
        public TableDataItem(string period, string revenue, string profit, string clients, string conversion, string margin, TableDataStatus status)
        {
            Period = period;
            Revenue = revenue;
            Profit = profit;
            Clients = clients;
            Conversion = conversion;
            Margin = margin;
            Status = status;
        }

        public string Period { get; set; }
        public string Revenue { get; set; }
        public string Profit { get; set; }
        public string Clients { get; set; }
        public string Conversion { get; set; }
        public string Margin { get; set; }
        public TableDataStatus Status { get; set; }

        public string StatusDisplay =>
            this.Status.GetType()
                .GetMember(this.Status.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? "";
    }
}