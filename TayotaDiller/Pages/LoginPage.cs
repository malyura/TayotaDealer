using Framework;
using Framework.Elements;
using OpenQA.Selenium;

namespace TayotaDealer.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly By _mainPageLoc = By.Id("login-form");
        private readonly TextBox _userNameTxb = new TextBox(By.Id("username"), "Username");
        private readonly TextBox _passwordTxb = new TextBox(By.Id("password"), "Password");
        private readonly Button _loginBtn = new Button(By.XPath("//button[contains(@class, 'ladda-button')]"), "Login");

        public LoginPage() : base(new Label(_mainPageLoc, "Login page"))
        {
        }

        public void TypeUserName(string userName)
        {
            _userNameTxb.Type(userName);
        }

        public void TypePassword(string password)
        {
            _passwordTxb.Type(password);
        }

        public void ClickLogin()
        {
            _loginBtn.Click();
        }
    }
}
