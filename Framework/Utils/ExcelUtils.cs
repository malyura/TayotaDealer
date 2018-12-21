using System.IO;
using System.Collections.Generic;
using ExcelDataReader;

namespace Framework.Utils
{
    public class ExcelUtils
    {
        public static Dictionary<int, List<string>> ReadExcel(string filePath, bool hasHeaderRecord)
        {
            var excelValues = new Dictionary<int, List<string>>();
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                var countRows = 0;

                while (excelReader.Read())
                {
                    var rowValues = new List<string>();

                    if (countRows != 0 || !hasHeaderRecord)
                    {
                        for (var i = 0; i < excelReader.FieldCount; i++)
                        {
                            rowValues.Add(excelReader.GetValue(i) == null ? string.Empty : excelReader.GetValue(i).ToString());
                        }

                        excelValues.Add(countRows, rowValues);
                    }

                    countRows++;
                }
            }

            return excelValues;
        }
    }
}
