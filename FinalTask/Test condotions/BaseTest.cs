using FinalTask.Factories;
using FinalTask.Util;
using FinalTask.WebDriver;
using Serilog;
using Log = Serilog.Log;

namespace FinalTask.Test_condotions
{
    public class BaseTest
    {
        [ThreadStatic]
        private static WebDriverWrapper? _browser;

        public WebDriverWrapper Browser => _browser!;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Configuration.DefaultDirectory + "/Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("========= Tests started =========");
        }

        [SetUp]
        public void Setup()
        {
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);

            _browser = new WebDriverWrapper(browserType);
            _browser.StartBrowser();
            _browser.NavigateTo(Configuration.AppUrl);
            Log.Information("Browser started and navigated to {Url}", Configuration.AppUrl);
        }

        [TearDown]
        public void Teardown()
        {
            _browser?.Close();
            _browser = null;
            Log.Information("Closing browser.");
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Log.Information("========= Tests finished =========");
        }
    }
}
