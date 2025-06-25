using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace FinalTask.Factories
{
    public static class BrowserFactory
    {
        public static IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("--incognito");

                        return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}
