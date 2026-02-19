using System.Text.Json.Serialization;

namespace KPI.Pulse.UI.Models.Settings
{
    public class KpiConfig
    {
        [JsonPropertyName("ids")]
        public int[] Ids { get; set; } = [];
    }
}
