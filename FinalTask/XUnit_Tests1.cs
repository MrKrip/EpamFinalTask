using FinalTask.Factories;
using FinalTask.Pages;
using FinalTask.Util;
using FinalTask.WebDriver;
using FluentAssertions;
using Serilog;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;


namespace FinalTask
{
    public class XUnit_Tests1 : IDisposable
    {
        private readonly WebDriverWrapper Browser;        

        static XUnit_Tests1()
        {
            Log.Logger = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.File(Configuration.DefaultDirectory + "/Logs/log-.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            Log.Information("========= Tests started =========");
        }

        public XUnit_Tests1()
        {
            Browser = new WebDriverWrapper((BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType));
            Browser.StartBrowser();
            Browser.NavigateTo(Configuration.AppUrl);
            Log.Information("Browser started and navigated to {Url}", Configuration.AppUrl);
        }

        public void Dispose()
        {
            Browser?.Close();
            Log.Information("Closing browser.");
        }

        public static IEnumerable<object[]> LoginData => new List<object[]>
        {
            new object[] { "standard_user", "secret_sauce" },
            new object[] { "locked_out_user", "secret_sauce" },
            new object[] { "problem_user", "secret_sauce" },
            new object[] { "performance_glitch_user", "secret_sauce" },
            new object[] { "error_user", "secret_sauce" },
            new object[] { "visual_user", "secret_sauce" }
        };

        [Theory]
        [MemberData(nameof(LoginData))]
        public void LoginWithoutUsernameAndPasswordTest(string userName, string password)
        {
            LoginPage loginPage = new LoginPage(Browser);
            string ExpectedErrorMessage = "Username is required";

            var ActualErrorMessage = loginPage.EnterUserName(userName)
                .EnterPassword(password)
                .ClearUserInput()
                .ClearPassword()
                .ClickLoginButton()
                .GetErrorMessage();

            ActualErrorMessage.Should().Contain(ExpectedErrorMessage);
        }
    }
}
