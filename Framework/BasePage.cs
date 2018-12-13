using System;
using OpenQA.Selenium;
using Framework.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium.Support.UI;

namespace Framework
{
    public abstract class BasePage
    {
        protected IWebDriver driver = Browser.GetDriver();
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected WebDriverWait wait;

        protected BasePage(BaseElement element)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Config.ExplicitlyWait));
            Assert.IsTrue(element.IsPresent(), "Element is not present");
            element.WaitElementIsVisible();
        }

        protected void LoadPage(string url)
        {
            logger.Info("Go to url " + url);
            Browser.Navigate(url);
        }
    }
}
