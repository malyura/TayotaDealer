using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace Framework.Utils
{
    public class ExcelUtils
    {
        public static Dictionary<int, List<string>> ReadExcel(string filePath, bool hasHeaderRecord)
        {
            var map = new Dictionary<int, List<string>>();
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                var countRows = 0;
                while (excelReader.Read())
                {
                    var lst = new List<string>();
                    if (countRows != 0 || !hasHeaderRecord)
                    {
                        for (var i = 0; i < excelReader.FieldCount; i++)
                        {
                            lst.Add(excelReader.GetValue(i) == null ? string.Empty : excelReader.GetValue(i).ToString());
                        }
                        map.Add(countRows, lst);
                    }

                    countRows++;
                }
            }

            return map;
        }
    }
}
