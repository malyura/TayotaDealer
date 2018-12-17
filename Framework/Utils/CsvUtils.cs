using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using OpenQA.Selenium.Support.UI;

namespace Framework.Utils
{
    public class CsvUtils
    {
        private static readonly WebDriverWait wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Config.ExplicitlyWait));

        public static Dictionary<int, List<string>> ReadCsv(string filePath, bool hasHeaderRecord)
        {
            var map = new Dictionary<int, List<string>>();
            using (var fileReader = File.OpenText(filePath))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.HasHeaderRecord = hasHeaderRecord;
                var k = 0;
                wait.Until(result => csv.Read());
                while (csv.Read())
                {
                    var lst = new List<string>();
                    for (var i = 0; csv.TryGetField(i, out string value); i++)
                    {
                        lst.Add(value);
                    }
                    map.Add(k, lst);
                    k++;
                }
            }

            return map;
        }
    }
}
