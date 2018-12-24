using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using Framework.Enums;
using Framework.Utilities;

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
                        firefoxOptions.SetPreference("browser.download.dir", Config.DownloadsDirectory);
                        firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");
                        _driver = new FirefoxDriver(firefoxOptions);
                        break;

                    case BrowserTypes.Chrome:
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddUserProfilePreference("download.default_directory", Config.DownloadsDirectory);
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
                _driver.Quit();
                _driver = null;
            }
        }

        public static void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public static string TakeScreenshot()
        {
            var screenshotsDriver = _driver as ITakesScreenshot;
            var screenshots = screenshotsDriver?.GetScreenshot();
            FileUtilities.CreateDirectoryIfNotExists(Config.ScreenshotsDirectory);
            var screenshotPath = Path.Combine(Config.ScreenshotsDirectory,
                new TimeUtilities().GetTimeNow(TimeFormat.TimeStamp) + FileType.PngImage.GetEnumDescription());
            screenshots?.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            return screenshotPath;
        }

        public static string GetPageSource()
        {
            return _driver.PageSource;
        }

        public static void SetImplicitWaitTimeOut(double timeout)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
        }
    }
}
