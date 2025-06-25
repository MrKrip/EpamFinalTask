using FinalTask.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FinalTask.WebDriver
{
    public class WebDriverWrapper
    {
        private readonly TimeSpan _timeout;

        private readonly IWebDriver _driver;

        private const int WaitTimeInSeconds = 10;

        public WebDriverWrapper(BrowserType browserType)
        {
            _driver = BrowserFactory.CreateWebDriver(browserType);
            _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }

        public void StartBrowser(int implicitWaitTime = 10)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);
        }

        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void WindowMaximize()
        {
            _driver.Manage().Window.Maximize();
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetUrl()
        {
            return _driver.Url;
        }

        public void Click(By by)
        {
            WaitForElementToBePresent(_driver, by, _timeout)?.Click();
        }

        public void EnterText(By by, string text)
        {
            var element = WaitForElementToBePresent(_driver, by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }

        public void ClearText(By by)
        {
            var element = WaitForElementToBePresent(_driver, by, _timeout);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Backspace);

            WebDriverWait wait = new WebDriverWait(_driver, _timeout);
            wait.Until(el => element.GetAttribute("value") == "");
        }

        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var elementPresent = WaitForElementToBePresent(_driver, by, _timeout);
            return _driver.FindElements(by);
        }

        public IWebElement FindElement(By by)
        {
            var elementPresent = WaitForElementToBePresent(_driver, by, _timeout);
            return elementPresent;
        }

        public IWebElement FindChildByName(By byParent, string childName)
        {
            var elementParent = WaitForElementToBePresent(_driver, byParent, _timeout);
            return elementParent.FindElement(By.Name(childName));
        }

        public void ClickAndSendAction(IWebElement element, string textToSend)
        {
            var clickAndSendKeysActions = new Actions(_driver);
            clickAndSendKeysActions.Click(element)
                .Pause(TimeSpan.FromSeconds(1))
                .SendKeys(textToSend)
                .Perform();
        }

        public static IWebElement WaitForElementToBePresent(IWebDriver Driver, By by, TimeSpan _timeout)
        {
            var wait = new WebDriverWait(Driver, _timeout);
            return wait.Until(drv =>
            {
                try
                {
                    var element = drv.FindElement(by);
                    if (element != null && element.Displayed)
                        return element;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
                }

                return null;
            });
        }
    }
}
