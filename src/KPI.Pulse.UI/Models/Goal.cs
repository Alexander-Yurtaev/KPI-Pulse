using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Pulse.UI.Models
{
    public class Goal
    {
        public Goal(string icon, string title, double value, double max, string description)
        {
            Icon = icon;
            Title = title;
            Value = value;
            Max = max;
            Description = description;
        }

        public string Icon { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }
        public double Max { get; set; }
        public string Description { get; set; }
    }
}
