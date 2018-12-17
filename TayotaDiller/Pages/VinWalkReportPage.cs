using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework.Elements;
using OpenQA.Selenium;
using Framework;
using Framework.Enums;
using Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TayotaDealer.Enums;
using TayotaDealer.Models;

namespace TayotaDealer.Pages
{
    public class VinWalkReportPage : BasePage
    {
        private static readonly By _vinWalkReportPageLoc =
            By.XPath($"//*[normalize-space(@class)='location-table-text' and normalize-space(.)='{ReportNames.VinWalk.GetEnumDescription()}']");
        private readonly string _vinWalkReportTableColumnLoc = "//th[@id='visualization-Tabular-th-{0}']";
        private readonly string _activeSortVinWalkTableColumnAscendingLoc = 
            "//th[@id='visualization-Tabular-th-{0}' and contains(@class, 'active') and contains(@class, 'asc')]";
        private readonly string _vinWalkTableItemsLoc = "//div[@id='visualization-Tabular']//tbody//td[{0}]";
        private readonly string _exportBtnLocator = "export-{0}";
        private readonly Button _updateResultsBtn = new Button(By.XPath("//button[contains(@class, 'submit-button') and not (@disabled)]"),
            "Update results");

        public VinWalkReportPage() : base(new Label(_vinWalkReportPageLoc, "VIN walk report page"))
        {
        }

        public bool IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns column)
        {
            return new Label(By.XPath(string.Format(_activeSortVinWalkTableColumnAscendingLoc, column)),
                $"Active sort ascending for {column.ToString()} column").IsPresent();
        }

        public void ClickUpdateResultsBtn()
        {
            _updateResultsBtn.Click();
        }

        public void ClickVinWalkReportTableColumnLbl(VinWalkTableColumns column)
        {
            GetVinWalkReportTableColumnLbl(column).Click();
        }

        public bool IsCorrectSortAscending(VinWalkTableColumns column)
        {
            var vinWalkReports = GetVinWalkReportItems();
            for (var i = 0; i < vinWalkReports.Count - 1; i++)
            {
                if (vinWalkReports[i].Year > vinWalkReports[i + 1].Year)
                {
                    Logger.Warn($"VIN Walk report item with VIN value {vinWalkReports[i].VIN} incorrectly sorted");
                    return false;
                }
            }

            return true;
        }

        public string DownloadVinWalkReportFile(FileTypes fileType)
        {
            FileUtils.DeleteDirectoryIfExists(Config.DownloadsDir);
            FileUtils.CreateDirectoryIfNotExists(Config.DownloadsDir);
            new Button(By.Id(string.Format(_exportBtnLocator, fileType.ToString())), $"{fileType} export").Click();
            Wait.Until(result => FileUtils.IsOverDownloadFile(Config.DownloadsDir, Directory.GetFiles(Config.DownloadsDir).FirstOrDefault(),
                fileType.GetEnumDescription()));
            return Directory.GetFiles(Config.DownloadsDir).First();
        }

        public void CheckExportToFile(string fileName, FileTypes type)
        {
            Assert.IsTrue(File.Exists(fileName), $"File {fileName} doesn't exist");
            var itemsFromPageTable = GetVinWalkReportItems();
            Assert.IsNotNull(itemsFromPageTable, "Report from VIN Walk report table has value null");
            var itemsFromFile = GetVinWalkReportItemsFromFile(type);
            Assert.IsNotNull(itemsFromFile, $"Report from {type} file with VIN Walk report has value null");
            for (var i = 0; i < itemsFromPageTable.Count; i++)
            {
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].VIN, itemsFromFile[i].VIN, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Year.ToString(), itemsFromFile[i].Year.ToString(), type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Make, itemsFromFile[i].Make, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Model, itemsFromFile[i].Model, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Trim, itemsFromFile[i].Trim, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].MMR, itemsFromFile[i].MMR, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Mileage, itemsFromFile[i].Mileage, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Location, itemsFromFile[i].Location, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Condition, itemsFromFile[i].Condition, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Color, itemsFromFile[i].Color, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Content, itemsFromFile[i].Content, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].CarFax, itemsFromFile[i].CarFax, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Structural, itemsFromFile[i].Structural, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].TimesRun, itemsFromFile[i].TimesRun, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].SalesChanel, itemsFromFile[i].SalesChanel, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Misc, itemsFromFile[i].Misc, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Manual, itemsFromFile[i].Manual, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Floor, itemsFromFile[i].Floor, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].Status, itemsFromFile[i].Status, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].SalePrice, itemsFromFile[i].SalePrice, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].SoldDate, itemsFromFile[i].SoldDate, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].PricingRule, itemsFromFile[i].PricingRule, type);
                CheckEqualVinWalkReportItems(itemsFromPageTable[i].DatePriced, itemsFromFile[i].DatePriced, type); 
            }
        }

        private static void CheckEqualVinWalkReportItems(string itemFromTable, string itemFromFile, FileTypes type)
        {
            SoftAssert.AreEqual(itemFromTable, itemFromFile, $"Items from VIN Walk report table and VIN Walk report file {type} aren't equal");
        }

        private Label GetVinWalkReportTableColumnLbl(VinWalkTableColumns column)
        {
            return new Label(By.XPath(string.Format(_vinWalkReportTableColumnLoc, column)), column.ToString());
        }

        private List<string> GetTextFromVinWalkTableItems(VinWalkTableColumns column)
        {
            var reportElements = new ListElements(By.XPath(string.Format(_vinWalkTableItemsLoc, (int) column)), $"{column} column items");

            return reportElements.GetTextFromListElements();
        }

        private List<VinWalkReport> GetVinWalkReportItems()
        {
            var vinValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.VIN);
            var yearValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Year);
            var makeValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Make);
            var modelValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Model);
            var trimValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Trim);
            var mmrValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.MMR);
            var mileageValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Mileage);
            var locationValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Location);
            var conditionValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Condition);
            var colorValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Color);
            var contentValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Content);
            var carFaxValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.CarFax);
            var structuralValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Structual);
            var timesRunValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.TimesRun);
            var salesChanelValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SalesChanel);
            var miscValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Misk);
            var manualValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Manual);
            var floorValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Floor);
            var statusValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Status);
            var salePriceValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SalePrice);
            var soldDateValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SoldDate);
            var pricingRuleValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.PricingRule);
            var datePricedValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.DatePriced);

            var reportItems = vinValues.Select((t, i) => new VinWalkReport
            {
                VIN = t,
                Year = int.Parse(yearValues[i]),
                Make = makeValues[i],
                Model = modelValues[i],
                Trim = trimValues[i],
                MMR = mmrValues[i],
                Mileage = mileageValues[i].Split('\r').First(),
                Location = locationValues[i].Split('\r').First(),
                Condition = conditionValues[i].Split('\r').First(),
                Color = colorValues[i].Split('\r').First(),
                Content = contentValues[i].Split('\r').First(),
                CarFax = carFaxValues[i].Split('\r').First(),
                Structural = structuralValues[i].Split('\r').First(),
                TimesRun = timesRunValues[i].Split('\r').First(),
                SalesChanel = salesChanelValues[i].Split('\r').First(),
                Misc = miscValues[i],
                Manual = manualValues[i],
                Floor = floorValues[i],
                Status = statusValues[i],
                SalePrice = salePriceValues[i],
                SoldDate = soldDateValues[i],
                PricingRule = pricingRuleValues[i].Replace("\r\n", "; "),
                DatePriced = datePricedValues[i]

            }).ToList();

            return reportItems;
        }

        private static List<VinWalkReport> GetVinWalkReportItemsFromFile(FileTypes type)
        {
            var filePath = Directory.GetFiles(Config.DownloadsDir).First();
            Dictionary<int, List<string>> map;
            switch (type)
            {
                case FileTypes.Csv:
                {
                    map = CsvUtils.ReadCsv(filePath, true);
                    break;
                }
                case FileTypes.Excel:
                {
                    map = ExcelUtils.ReadExcel(filePath, true);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            var reportItems = new List<VinWalkReport>();

            foreach (var lst in map.Values)
            {
                var report = new VinWalkReport
                {
                    VIN = lst[GetColumnIndexForFile(VinWalkTableColumns.VIN)],
                    Year = int.Parse(lst[GetColumnIndexForFile(VinWalkTableColumns.Year)]),
                    Make = lst[GetColumnIndexForFile(VinWalkTableColumns.Make)],
                    Model = lst[GetColumnIndexForFile(VinWalkTableColumns.Model)],
                    Trim = lst[GetColumnIndexForFile(VinWalkTableColumns.Trim)],
                    MMR = lst[GetColumnIndexForFile(VinWalkTableColumns.MMR)],
                    Mileage = lst[GetColumnIndexForFile(VinWalkTableColumns.Mileage)],
                    Location = lst[GetColumnIndexForFile(VinWalkTableColumns.Location)],
                    Condition = lst[GetColumnIndexForFile(VinWalkTableColumns.Condition)],
                    Color = lst[GetColumnIndexForFile(VinWalkTableColumns.Color)],
                    Content = lst[GetColumnIndexForFile(VinWalkTableColumns.Content)],
                    CarFax = lst[GetColumnIndexForFile(VinWalkTableColumns.CarFax)],
                    Structural = lst[GetColumnIndexForFile(VinWalkTableColumns.Structual)],
                    TimesRun = lst[GetColumnIndexForFile(VinWalkTableColumns.TimesRun)],
                    SalesChanel = lst[GetColumnIndexForFile(VinWalkTableColumns.SalesChanel)],
                    Misc = lst[GetColumnIndexForFile(VinWalkTableColumns.Misk)],
                    Manual = lst[GetColumnIndexForFile(VinWalkTableColumns.Manual)],
                    Floor = lst[GetColumnIndexForFile(VinWalkTableColumns.Floor)],
                    Status = lst[GetColumnIndexForFile(VinWalkTableColumns.Status)],
                    SalePrice = lst[GetColumnIndexForFile(VinWalkTableColumns.SalePrice)],
                    SoldDate = lst[GetColumnIndexForFile(VinWalkTableColumns.SoldDate)],
                    PricingRule = lst[GetColumnIndexForFile(VinWalkTableColumns.PricingRule)],
                    DatePriced = lst[GetColumnIndexForFile(VinWalkTableColumns.DatePriced)]
                };
                reportItems.Add(report);
            }

            return reportItems;
        }

        private static int GetColumnIndexForFile(VinWalkTableColumns tableColumn) => (int) tableColumn - 1;
    }
}
