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
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.SetPreference("browser.download.folderList", 2);
                        firefoxOptions.SetPreference("browser.download.dir", Config.DownloadsDir);
                        firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");
                        _driver = new FirefoxDriver(firefoxOptions);
                        break;

                    case BrowserTypes.Chrome:
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddUserProfilePreference("download.default_directory", Config.DownloadsDir);
                        chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                        _driver = new ChromeDriver(chromeOptions);
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
                //_driver.Quit();
                _driver = null;
            }
        }

        public static void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
    }
}
