using System;
using System.Configuration;

namespace Framework
{
    public class Config
    {
        public static string Url => GetConfiguration("url");

        public static int ImplicitlyWait => GetConfigurationToInt("implicit_wait");

        public static int ExplicitlyWait => GetConfigurationToInt("explicit_wait");

        public static string UserName => GetConfiguration("username");

        public static string Password => GetConfiguration("password");

        public static string DownloadsDir => GetConfiguration("downloads_dir"); 

        public static BrowserTypes Browser => Enum.TryParse(GetConfiguration("browser"), true, out BrowserTypes val) ? val : BrowserTypes.Unknown;

        private static string GetConfiguration(string value) => ConfigurationManager.AppSettings[value];

        private static int GetConfigurationToInt(string value) => Convert.ToInt32(ConfigurationManager.AppSettings[value]);
    }
}