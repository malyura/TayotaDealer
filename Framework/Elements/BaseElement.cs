using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Elements
{
    public class BaseElement
    {
        protected IWebDriver Driver = Browser.GetDriver();
        protected Logger Logger = LogManager.GetCurrentClassLogger();
        protected WebDriverWait Wait;
        protected By Locator;
        protected string Description;

        protected BaseElement(By locator, string description)
        {
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.ExplicitlyWait));
            this.Locator = locator;
            this.Description = description;
        }

        public void Click()
        {
            WaitElementIsVisible();
            Logger.Info("Click " + Description);
            GetElement().Click();
        }

        public string GetAttribute(string attributeName)
        {
            WaitElementIsVisible();
            return GetElement().GetAttribute(attributeName);
        }

        public By GetLocator() => Locator;

        public IWebElement GetElement() => Driver.FindElement(Locator);
       
        public string GetText()
        {
            WaitElementIsVisible();
            return GetElement().Text;
        }

        public bool IsPresent() => Driver.FindElements(Locator).Count > 0;

        public void WaitElementIsVisible()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(Locator));
        }
    }
}
