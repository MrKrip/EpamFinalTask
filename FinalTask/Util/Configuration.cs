
using Microsoft.Extensions.Configuration;

namespace FinalTask.Util
{
    public class Configuration
    {
        public static string? BrowserType { get; private set; }

        public static string? AppUrl { get; private set; }
        public static string? DefaultDirectory { get; private set; }

        static Configuration() => Init();

        public static void Init()
        {
            DefaultDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(DefaultDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            BrowserType = configuration["BrowserType"] ?? "Chrome";
            AppUrl = configuration["ApplicationUrl"] ?? string.Empty;
        }
    }
}
