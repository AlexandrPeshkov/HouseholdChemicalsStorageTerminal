using System.IO;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using DataAccess.Interfaces;
using Interfaces.Services;
using UniRx;
using UnityEngine;

namespace Core.Services
{
    public class AppConfigProvider : IAsyncInitializable, IDbConfigProvider
    {
        private const string _configFileName = "run-config.cfg";

        private readonly string _configPath;

        private readonly Encoding _encoding;

        private readonly DbConfig _seedConfig;

        private readonly DbConfig _defaultConfig;

        private readonly ReactiveProperty<DbConfig> _appConfig;

        public int Order => -999;

        public bool IsReady { get; private set; }

        public IReadOnlyReactiveProperty<DbConfig> AppConfig { get; }

        public AppConfigProvider()
        {
            _seedConfig = new DbConfig { ClearAndSeedDb = true, UseConfig = true };
            _defaultConfig = new DbConfig { ClearAndSeedDb = false, UseConfig = true };

            _appConfig = new ReactiveProperty<DbConfig>(_seedConfig);
            AppConfig = _appConfig;
            _encoding = Encoding.UTF8;
            _configPath = Path.Combine(Application.persistentDataPath, _configFileName);
        }

        public async Task Initialize()
        {
            await ParseConfig();
            IsReady = true;
        }

        private async Task ParseConfig()
        {
            if (File.Exists(_configPath))
            {
                string json = null;

                using (var fs = new FileStream(_configPath, FileMode.Open))
                {
                    var stringBuilder = new StringBuilder();
                    var buf = new byte[1024];
                    int size;

                    while ((size = await fs.ReadAsync(buf, 0, buf.Length)) > 0)
                    {
                        var part = _encoding.GetString(buf, 0, size);
                        stringBuilder.Append(part);
                    }

                    json = stringBuilder.ToString();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    var config = JsonUtility.FromJson<DbConfig>(json);

                    if (config.UseConfig)
                    {
                        _appConfig.Value = config;
                    }
                    else
                    {
                        _appConfig.Value = _defaultConfig;
                    }
                }
            }
            else
            {
                var json = JsonUtility.ToJson(_seedConfig);
                _appConfig.Value = _seedConfig;

                using (var fs = new FileStream(_configPath, FileMode.Create))
                {
                    using (var sr = new StreamWriter(fs))
                    {
                        await sr.WriteAsync(json);
                    }
                }
            }
        }
    }
}