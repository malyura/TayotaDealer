using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;

namespace Framework
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver Driver = Browser.GetDriver();
        protected Logger Logger = LogManager.GetCurrentClassLogger();

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Logger.Debug("Add implicitly wait");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitlyWait);
            Logger.Debug("Maximize window browser");
            Driver.Manage().Window.Maximize();
            Logger.Debug("Navigate to URL");
            Browser.Navigate(Config.Url);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                Browser.TakeScreenshot();
            }

            Logger.Debug("Quit browser");
            Browser.Quit();
        }
    }
}
