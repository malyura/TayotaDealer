using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Elements
{
    public class BaseElement
    {
        protected IWebDriver driver = Browser.GetDriver();
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected WebDriverWait wait;
        protected By locator;
        protected string description;

        protected BaseElement(By locator, string description)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Config.ExplicitlyWait));
            this.locator = locator;
            this.description = description;
        }

        public void Click()
        {
            WaitElementIsVisible();
            logger.Info("Click " + description);
            GetElement().Click();
        }

        public string GetAttribute(string attributeName)
        {
            WaitElementIsVisible();
            return GetElement().GetAttribute(attributeName);
        }

        public By GetLocator() => locator;

        public IWebElement GetElement() => driver.FindElement(locator);
       
        public string GetText()
        {
            WaitElementIsVisible();
            return GetElement().Text;
        }

        public bool IsPresent() => driver.FindElements(locator).Count > 0;

        public void WaitElementIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
    }
}
