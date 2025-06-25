using FinalTask.WebDriver;
using Serilog;

namespace FinalTask.Pages
{
    public class MainPage
    {
        private readonly WebDriverWrapper driver;

        public MainPage(WebDriverWrapper driver)
        {
            this.driver = driver;
        }

        public string GetTitle()
        {
            Log.Information("Get title");
            return driver.GetTitle();
        }
    }
}
