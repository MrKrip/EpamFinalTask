using FinalTask.WebDriver;
using OpenQA.Selenium;
using Serilog;

namespace FinalTask.Pages
{
    public class LoginPage
    {
        private readonly WebDriverWrapper driver;

        private readonly By userInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By ErrorMessage = By.XPath("//div[contains(@class,'error')]//h3");

        public LoginPage(WebDriverWrapper driver)
        {
            this.driver = driver;
        }

        public bool PageIsOpen()
        {
            return driver.FindElements(userInput).Count > 0 && driver.FindElements(passwordInput).Count > 0;
        }


        public LoginPage EnterUserName(string userName)
        {
            driver.EnterText(userInput, userName);
            Log.Information($"Username {userName} entered in input field");
            return this;
        }

        public LoginPage ClearUserInput()
        {
            driver.ClearText(userInput);
            Log.Information("Cleared username input");
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            driver.EnterText(passwordInput, password);
            Log.Information($"Password entered in input field");
            return this;
        }

        public LoginPage ClearPassword()
        {
            driver.ClearText(passwordInput);
            Log.Information("Cleared password input");
            return this;
        }

        public LoginPage ClickLoginButton()
        {
            driver.Click(loginButton);
            Log.Information("The login button has been pressed.");
            return this;
        }

        public string GetErrorMessage()
        {
            Log.Information("Getting error message");
            return driver.FindElement(ErrorMessage).Text;
        }

        public MainPage Login(string username, string password)
        {
            EnterUserName(username);
            EnterPassword(password);
            ClickLoginButton();
            return new MainPage(driver);
        }
    }
}
