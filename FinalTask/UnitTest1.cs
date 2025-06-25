using FinalTask.Pages;
using FinalTask.Test_condotions;
using FluentAssertions;

namespace FinalTask
{
    [Parallelizable(ParallelScope.All)]
    public class Tests : BaseTest
    {
        [Test]
        [TestCaseSource(nameof(LoginData))]
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

        [Test]
        [TestCaseSource(nameof(LoginData))]
        public void LoginWithoutPasswordTest(string userName, string password)
        {
            LoginPage loginPage = new LoginPage(Browser);
            string ExpectedErrorMessage = "Password is required";

            var ActualErrorMessage = loginPage.EnterUserName(userName)
                .EnterPassword(password)
                .ClearPassword()
                .ClickLoginButton()
                .GetErrorMessage();

            ActualErrorMessage.Should().Contain(ExpectedErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(LoginData))]
        public void MainPageTitleTest(string userName, string password)
        {
            LoginPage LoginPage = new LoginPage(Browser);
            string ExpectedTitle = "Swag Labs";

            var ActualTitle = LoginPage.Login(userName, password).GetTitle();

            ActualTitle.Should().Be(ExpectedTitle);
        }

        private static IEnumerable<TestCaseData> LoginData()
        {
            yield return new TestCaseData("standard_user", "secret_sauce");
            yield return new TestCaseData("locked_out_user", "secret_sauce");
            yield return new TestCaseData("problem_user", "secret_sauce");
            yield return new TestCaseData("performance_glitch_user", "secret_sauce");
            yield return new TestCaseData("error_user", "secret_sauce");
            yield return new TestCaseData("visual_user", "secret_sauce");
        }
    }
}