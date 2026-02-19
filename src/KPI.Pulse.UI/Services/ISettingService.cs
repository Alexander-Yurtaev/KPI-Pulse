using KPI.Pulse.UI.Models.Settings;
using System.Threading.Tasks;

namespace KPI.Pulse.UI.Services;

public interface ISettingService
{
    Task SaveAsync(Config config);
    Task<Config> LoadAsync();
}