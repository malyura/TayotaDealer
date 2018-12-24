using System;
using System.Collections.Generic;
using Framework.Enums;
using Framework.Utilities;
using ToyotaDealer.Enums;
using ToyotaDealer.Models;

namespace ToyotaDealer.Helpers
{
    public class FileReadingHelper
    {
        public static List<VinWalkReport> GetVinWalkReportItemsFromFile(string filePath, FileType type)
        {
            Dictionary<int, List<string>> valuesFromFile;

            switch (type)
            {
                case FileType.Csv:
                    {
                        valuesFromFile = CsvUtilities.ReadCsv(filePath, true);
                        break;
                    }

                case FileType.Excel:
                    {
                        valuesFromFile = ExcelUtilities.ReadExcel(filePath, true);
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
                    Vin = values[GetColumnIndexForFile(VinWalkTableColumn.Vin)],
                    Year = int.Parse(values[GetColumnIndexForFile(VinWalkTableColumn.Year)]),
                    Make = values[GetColumnIndexForFile(VinWalkTableColumn.Make)],
                    Model = values[GetColumnIndexForFile(VinWalkTableColumn.Model)],
                    Trim = values[GetColumnIndexForFile(VinWalkTableColumn.Trim)],
                    Mmr = values[GetColumnIndexForFile(VinWalkTableColumn.Mmr)],
                    Mileage = values[GetColumnIndexForFile(VinWalkTableColumn.Mileage)],
                    Location = values[GetColumnIndexForFile(VinWalkTableColumn.Location)],
                    Condition = values[GetColumnIndexForFile(VinWalkTableColumn.Condition)],
                    Color = values[GetColumnIndexForFile(VinWalkTableColumn.Color)],
                    Content = values[GetColumnIndexForFile(VinWalkTableColumn.Content)],
                    CarFax = values[GetColumnIndexForFile(VinWalkTableColumn.CarFax)],
                    Structural = values[GetColumnIndexForFile(VinWalkTableColumn.Structural)],
                    TimesRun = values[GetColumnIndexForFile(VinWalkTableColumn.TimesRun)],
                    SalesChanel = values[GetColumnIndexForFile(VinWalkTableColumn.SalesChanel)],
                    Misc = values[GetColumnIndexForFile(VinWalkTableColumn.Misc)],
                    Manual = values[GetColumnIndexForFile(VinWalkTableColumn.Manual)],
                    Floor = values[GetColumnIndexForFile(VinWalkTableColumn.Floor)],
                    Status = values[GetColumnIndexForFile(VinWalkTableColumn.Status)],
                    SalePrice = values[GetColumnIndexForFile(VinWalkTableColumn.SalePrice)],
                    SoldDate = values[GetColumnIndexForFile(VinWalkTableColumn.SoldDate)],
                    PricingRule = values[GetColumnIndexForFile(VinWalkTableColumn.PricingRule)],
                    DatePriced = values[GetColumnIndexForFile(VinWalkTableColumn.DatePriced)]
                };

                reportItems.Add(report);
            }

            return reportItems;
        }

        private static int GetColumnIndexForFile(VinWalkTableColumn tableColumn) => (int)tableColumn - 1;
    }
}
