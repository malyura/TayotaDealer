using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;

namespace Framework
{
    public class BaseTest
    {
        protected IWebDriver driver = Browser.GetDriver();
        protected Logger logger = LogManager.GetCurrentClassLogger();

        [TestInitialize]
        public void TestInitialize()
        {
            logger.Debug("Add implicitly wait");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitlyWait);
            logger.Debug("Maximaze window browser");
            driver.Manage().Window.Maximize();
            logger.Debug("Navigate to URL");
            Browser.Navigate(Config.Url);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            logger.Debug("Quit browser");
            Browser.Quit();
        }
    }
}
