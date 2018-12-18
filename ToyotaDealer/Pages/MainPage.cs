using Framework;
using OpenQA.Selenium;
using Framework.Elements;
using ToyotaDealer.Menus;

namespace ToyotaDealer.Pages
{
    public class MainPage : BasePage
    {
        private static readonly By _mainPageLocator = By.XPath("//div[normalize-space(@data-module-name)='OptimizationDashboard']");

        public NavigationMenu NavigationMenu => new NavigationMenu();

        public MainPage() : base(new Element(_mainPageLocator, "Main page"))
        {
        }
    }
}
