using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;

namespace Framework
{
    public class BaseTest
    {
        protected IWebDriver Driver = Browser.GetDriver();
        protected Logger Logger = LogManager.GetCurrentClassLogger();

        [TestInitialize]
        public void TestInitialize()
        {
            Logger.Debug("Add implicitly wait");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitlyWait);
            Logger.Debug("Maximaze window browser");
            Driver.Manage().Window.Maximize();
            Logger.Debug("Navigate to URL");
            Browser.Navigate(Config.Url);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Logger.Debug("Quit browser");
            Browser.Quit();
        }
    }
}
