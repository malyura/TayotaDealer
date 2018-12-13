using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Framework
{
    public class Browser
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                switch (Config.Browser)
                {
                    case BrowserTypes.Firefox:
                        _driver = new FirefoxDriver();
                        break;

                    case BrowserTypes.Chrome:
                        _driver = new ChromeDriver();
                        break;

                    case BrowserTypes.Unknown:
                        throw new IndexOutOfRangeException("Unknown browser type");

                    default:
                        {
                            throw new IndexOutOfRangeException("Incorrect browser type");
                        }
                }
            }

            return _driver;
        }

        public static void Quit()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        public static void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
    }
}
