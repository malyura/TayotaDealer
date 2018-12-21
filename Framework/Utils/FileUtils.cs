using System;
using System.IO;
using System.Linq;
using Framework.Enums;
using OpenQA.Selenium.Support.UI;

namespace Framework.Utils
{
    public class FileUtils
    {
        private static readonly WebDriverWait _wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Config.ExplicitlyWait));

        public static void DeleteFileIfExists(string pathFile)
        {
            var file = new FileInfo(pathFile);

            if (file.Exists)
            {
                file.Delete();
                _wait.Until(result => !file.Exists);
            }
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                _wait.Until(result => Directory.Exists(path));
            }
        }

        public static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                _wait.Until(result => !Directory.Exists(path));
            }
        }

        public static bool IsExistsFileWithExtension(string pathFile, string extension)
        {
            return pathFile != null && Path.GetExtension(pathFile).Equals(extension);
        }

        public static bool IsOverDownloadFile(string downloadDir, string pathFile, string extension)
        {
            string downloadExtension = null;

            switch (Config.Browser)
            {
                case BrowserTypes.Chrome:
                    downloadExtension = FileTypes.ChromeDownload.GetEnumDescription();
                    break;

                case BrowserTypes.Firefox:
                    downloadExtension = FileTypes.FirefoxDownload.GetEnumDescription();
                    break;

                case BrowserTypes.Unknown:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return !Directory.EnumerateFiles(downloadDir, "*.*", SearchOption.TopDirectoryOnly).
                       Any(s => s.EndsWith(downloadExtension ?? throw new InvalidOperationException()))
                   && IsExistsFileWithExtension(pathFile, extension);
        }
    }
}
