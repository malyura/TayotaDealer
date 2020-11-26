using System;
using System.Configuration;
using System.IO;

namespace Framework
{
    public class Config
    {
        public static string Url => GetConfiguration("url");
        //dgdgdfgdfgdf

        public static int ImplicitlyWait => GetConfigurationToInt("implicit_wait");

        public static int ExplicitlyWait => GetConfigurationToInt("explicit_wait");

        public static string UserName => GetConfiguration("username");

        public static string Password => GetConfiguration("password");

        public static string DownloadsDirectory => Path.Combine(Directory.GetCurrentDirectory(), GetConfiguration("downloads_dir"));

        public static string ScreenshotsDirectory => Path.Combine(Directory.GetCurrentDirectory(), GetConfiguration("screenshots_dir"));

        public static BrowserTypes Browser => Enum.TryParse(GetConfiguration("browser"), true, out BrowserTypes value) ? value : BrowserTypes.Unknown;

        private static string GetConfiguration(string value) => ConfigurationManager.AppSettings[value];

        private static int GetConfigurationToInt(string value) => Convert.ToInt32(ConfigurationManager.AppSettings[value]);
    }
}