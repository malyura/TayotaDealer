using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace Framework.Utils
{
    public class FileUtils
    {
        private static readonly WebDriverWait wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Config.ExplicitlyWait));

        public static void DeleteFileIfExists(string pathFile)
        {
            var file = new FileInfo(pathFile);
            if (file.Exists)
            {
                file.Delete();
                wait.Until(result => !file.Exists);
            }
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                wait.Until(result => Directory.Exists(path));
            }
        }

        public static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                wait.Until(result => !Directory.Exists(path));
            }
        }

        public static bool IsExistsFileWithExtension(string pathFile, string extension)
        {
            return pathFile == null ? false : Path.GetExtension(pathFile).Equals(extension);
        }

        public static bool IsOverDownloadFile(string downloadDir, string pathFile, string extension)
        {
            return !Directory.EnumerateFiles(downloadDir, "*.*", SearchOption.TopDirectoryOnly).Any(s => s.EndsWith(".crdownload") || s.EndsWith(".part"))
                && IsExistsFileWithExtension(pathFile, extension);
        }
    }
}
