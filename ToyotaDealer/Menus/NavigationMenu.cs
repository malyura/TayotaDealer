using Framework;
using Framework.Elements;
using Framework.Utilities;
using OpenQA.Selenium;
using ToyotaDealer.Enums;

namespace ToyotaDealer.Menus
{
    public class NavigationMenu : BasePage
    {
        private static readonly By _navigationMenuLocator = By.ClassName("nav-list");
        private readonly string _navigationMenuItemLocator = "//a[normalize-space(@class)='nav-{0}']";

        public NavigationMenu() : base(new Element(_navigationMenuLocator, "Navigation menu"))
        {
        }

        public void ClickNavigationMenuItem(NavigationMenuItem item)
        {
            GetNavigationMenuItem(item).Click();
        }

        private Element GetNavigationMenuItem(NavigationMenuItem item)
        {
            return new Element(By.XPath(string.Format(_navigationMenuItemLocator, item.GetEnumDescription())), item.ToString());
        }
    }
}
