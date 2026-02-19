using System.Text.Json.Serialization;

namespace KPI.Pulse.UI.Models.Settings;

public class Config
{
    [JsonPropertyName("kpi")]
    public KpiConfig KpiConfig { get; set; } = new KpiConfig();
}