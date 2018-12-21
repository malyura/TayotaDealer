using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework;
using Framework.Enums;
using Framework.Utils;
using ToyotaDealer.Enums;
using ToyotaDealer.Models;

namespace ToyotaDealer.Helpers
{
    public class FileReadingHelper
    {
        public static List<VinWalkReport> GetVinWalkReportItemsFromFile(string filePath, FileTypes type)
        {
            Dictionary<int, List<string>> valuesFromFile;

            switch (type)
            {
                case FileTypes.Csv:
                    {
                        valuesFromFile = CsvUtils.ReadCsv(filePath, true);
                        break;
                    }

                case FileTypes.Excel:
                    {
                        valuesFromFile = ExcelUtils.ReadExcel(filePath, true);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var reportItems = new List<VinWalkReport>();

            foreach (var values in valuesFromFile.Values)
            {
                var report = new VinWalkReport
                {
                    Vin = values[GetColumnIndexForFile(VinWalkTableColumns.Vin)],
                    Year = int.Parse(values[GetColumnIndexForFile(VinWalkTableColumns.Year)]),
                    Make = values[GetColumnIndexForFile(VinWalkTableColumns.Make)],
                    Model = values[GetColumnIndexForFile(VinWalkTableColumns.Model)],
                    Trim = values[GetColumnIndexForFile(VinWalkTableColumns.Trim)],
                    Mmr = values[GetColumnIndexForFile(VinWalkTableColumns.Mmr)],
                    Mileage = values[GetColumnIndexForFile(VinWalkTableColumns.Mileage)],
                    Location = values[GetColumnIndexForFile(VinWalkTableColumns.Location)],
                    Condition = values[GetColumnIndexForFile(VinWalkTableColumns.Condition)],
                    Color = values[GetColumnIndexForFile(VinWalkTableColumns.Color)],
                    Content = values[GetColumnIndexForFile(VinWalkTableColumns.Content)],
                    CarFax = values[GetColumnIndexForFile(VinWalkTableColumns.CarFax)],
                    Structural = values[GetColumnIndexForFile(VinWalkTableColumns.Structural)],
                    TimesRun = values[GetColumnIndexForFile(VinWalkTableColumns.TimesRun)],
                    SalesChanel = values[GetColumnIndexForFile(VinWalkTableColumns.SalesChanel)],
                    Misc = values[GetColumnIndexForFile(VinWalkTableColumns.Misc)],
                    Manual = values[GetColumnIndexForFile(VinWalkTableColumns.Manual)],
                    Floor = values[GetColumnIndexForFile(VinWalkTableColumns.Floor)],
                    Status = values[GetColumnIndexForFile(VinWalkTableColumns.Status)],
                    SalePrice = values[GetColumnIndexForFile(VinWalkTableColumns.SalePrice)],
                    SoldDate = values[GetColumnIndexForFile(VinWalkTableColumns.SoldDate)],
                    PricingRule = values[GetColumnIndexForFile(VinWalkTableColumns.PricingRule)],
                    DatePriced = values[GetColumnIndexForFile(VinWalkTableColumns.DatePriced)]
                };

                reportItems.Add(report);
            }

            return reportItems;
        }

        private static int GetColumnIndexForFile(VinWalkTableColumns tableColumn) => (int)tableColumn - 1;
    }
}
