using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace Framework.Utils
{
    public class CsvUtils
    {

        public static Dictionary<int, List<string>> ReadCsv(string filePath, bool hasHeaderRecord)
        {
            var csvValues = new Dictionary<int, List<string>>();
            using (var fileReader = File.OpenText(filePath))
            {
                var csv = new CsvReader(fileReader);
                var countRows = 0;

                while (csv.Read())
                {
                    var rowValues = new List<string>();

                    if (countRows != 0 || !hasHeaderRecord)
                    {
                        for (var i = 0; csv.TryGetField(i, out string value); i++)
                        {
                            rowValues.Add(value);
                        }

                        csvValues.Add(countRows, rowValues);
                    }

                    countRows++;
                }
            }

            return csvValues;
        }
    }
}
