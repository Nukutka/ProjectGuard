using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace ProjectGuard.Services.Utils
{
    public class AppSettingsManager
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        static AppSettingsManager()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string environmentName = "Development")
        {
            var path = WebContentDirectoryFinder.CalculateContentRootFolder();
            var cacheKey = path + "#" + environmentName;
            return ConfigurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", true, true);

            if (!environmentName.IsNullOrWhiteSpace())
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", true);

            builder = builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
