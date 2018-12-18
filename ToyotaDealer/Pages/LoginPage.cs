using Framework;
using Framework.Elements;
using OpenQA.Selenium;

namespace ToyotaDealer.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly By _mainPageLocator = By.Id("login-form");
        private readonly TextBox _userNameTextBox = new TextBox(By.Id("username"), "Username");
        private readonly TextBox _passwordTextBox = new TextBox(By.Id("password"), "Password");
        private readonly Element _loginButton = new Element(By.XPath("//button[contains(@class, 'ladda-button')]"), "Login");

        public LoginPage() : base(new Element(_mainPageLocator, "Login page"))
        {
        }

        public void TypeUserName(string userName)
        {
            _userNameTextBox.Type(userName);
        }

        public void TypePassword(string password)
        {
            _passwordTextBox.Type(password);
        }

        public void ClickLogin()
        {
            _loginButton.Click();
        }
    }
}
