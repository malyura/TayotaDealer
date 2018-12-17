using Framework;
using OpenQA.Selenium;
using Framework.Elements;
using TayotaDealer.Menus;

namespace TayotaDealer.Pages
{
    public class MainPage : BasePage
    {
        private static readonly By _mainPageLoc = By.XPath("//div[normalize-space(@data-module-name)='OptimizationDashboard']");

        public NavigationMenu NavigationMenu => new NavigationMenu();

        public MainPage() : base(new Label(_mainPageLoc, "Main page"))
        {
        }
    }
}
