﻿using Framework;
using Framework.Elements;
using Framework.Utils;
using OpenQA.Selenium;
using TayotaDealer.Enums;

namespace TayotaDealer.Menus
{
    public class NavigationMenu : BasePage
    {
        private static readonly By _navigationMenuLoc = By.ClassName("nav-list");
        private readonly string _navigationMenuItemLoc = "//a[normalize-space(@class)='nav-{0}']";

        public NavigationMenu() : base(new Label(_navigationMenuLoc, "Navigation menu"))
        {
        }

        public void ClickNavigationMenuItem(NavigationMenuItems item)
        {
            GetNavigationMenuItem(item).Click();
        }

        private Label GetNavigationMenuItem(NavigationMenuItems item)
        {
            return new Label(By.XPath(string.Format(_navigationMenuItemLoc, EnumUtils.GetEnumDescription(item))), item.ToString());
        }
    }
}
