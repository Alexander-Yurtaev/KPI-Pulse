using KPI.Pulse.UI.Models.Settings;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace KPI.Pulse.UI.Services
{
    public class SettingService: ISettingService
    {
        private const string FileName = "Settings.json";

        public async Task SaveAsync(Config config)
        {
            await using FileStream fs = File.Create(FileName);
            await JsonSerializer.SerializeAsync(fs, config);
        }

        public async Task<Config> LoadAsync()
        {
            if (File.Exists(FileName))
            {
                await using FileStream fs = File.OpenRead(FileName);
                var person = await JsonSerializer.DeserializeAsync<Config>(fs);
                return person ?? new Config();
            }
            else
            {
                return new Config();
            }
        }
    }
}
