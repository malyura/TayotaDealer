using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
            Locator = locator;
            Description = description;
        }

        public void Click()
        {
            WaitElementIsVisible();
            Logger.Info("Click element " + $"'{Description}'");
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
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Locator));
        }

        public void WaitElementIsExists()
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(Locator));
        }

        public void MoveToElement()
        {
            WaitElementIsExists();
            Logger.Info("Move mouse to element " + $"'{Description}'");
            new Actions(Browser.GetDriver()).MoveToElement(GetElement()).Perform();
        }

        public void ClickJs()
        {
            WaitElementIsExists();
            Logger.Info("Click element " + $"'{Description}'");
            var executor = (IJavaScriptExecutor) Driver;
            executor.ExecuteScript("arguments[0].click();", GetElement());
        }
    }
}
