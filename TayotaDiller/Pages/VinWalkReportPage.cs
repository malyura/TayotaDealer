using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework.Elements;
using OpenQA.Selenium;
using Framework;
using Framework.Utils;
using TayotaDealer.Enums;
using TayotaDealer.Models;

namespace TayotaDealer.Pages
{
    public class VinWalkReportPage : BasePage
    {
        private const string TableItemsRegex = @"[^(\/)]*";
        private static readonly By _vinWalkReportPageLoc =
            By.XPath(string.Format("//*[normalize-space(@class)='location-table-text' and normalize-space(.)='{0}']", ReportNames.VinWalk.GetEnumDescription()));
        private readonly string _vinWalkReportTableColumnLoc = "//th[@id='visualization-Tabular-th-{0}']";
        private readonly string _activeSortVinWalkTableColumnAscendingLoc = 
            "//th[@id='visualization-Tabular-th-{0}' and contains(@class, 'active') and contains(@class, 'asc')]";
        private readonly string _vinWalkTableItemsLoc = "//div[@id='visualization-Tabular']//tbody//td[{0}]";
        private readonly Button _updateResultsBtn = new Button(By.XPath("//button[contains(@class, 'submit-button') and not (@disabled)]"),
            "Update results");
        private readonly Button _csvExportBtn = new Button(By.Id("export-Csv"), "Csv export");
        private readonly Button _xlsExportBtn = new Button(By.Id("export-Excel"), "Excel export");

        public VinWalkReportPage() : base(new Label(_vinWalkReportPageLoc, "VIN walk report page"))
        {
        }

        public bool IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns column)
        {
            return new Label(By.XPath(string.Format(_activeSortVinWalkTableColumnAscendingLoc, column)),
               string.Format("Active sort ascending for {0} column", column.ToString())).IsPresent();
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
                    logger.Warn(string.Format("VIN Walk report item with VIN value {0} incorrectly sorted", vinWalkReports[i].VIN));
                    return false;
                }
            }

            return true;
        }

        public void DownloadVinWalkReportFile(FileTypes fileType)
        {
            FileUtils.DeleteDirectoryIfExists(Config.DownloadsDir);
            FileUtils.CreateDirectoryIfNotExists(Config.DownloadsDir);
            _csvExportBtn.Click();
            wait.Until(result => FileUtils.IsOverDownloadFile(Config.DownloadsDir, Directory.GetFiles(Config.DownloadsDir).FirstOrDefault(),
                EnumUtils.GetEnumDescription(fileType)));
        }

        //public bool IsExistsFileWithType(FileTypes fileType)
        //{
        //    try
        //    {
        //        wait.Until(result => FileUtils.IsExistsFileWithExtension(Directory.GetFiles(Config.DownloadsDir).FirstOrDefault(),
        //            EnumUtils.GetEnumDescription(fileType)));
    
        //    }
        //    catch (WebDriverTimeoutException)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public void CheckReportFile(FileTypes type)
        {
            string dfds = StringUtils.GetMatch("dsfds/hjgj", TableItemsRegex);
            var itemsFromPageTable = GetVinWalkReportItems();
            

            var itemsFromFile = GetVinWalkReportItemsFromFile(type);
            string ff = "";
        }

        private Label GetVinWalkReportTableColumnLbl(VinWalkTableColumns column)
        {
            return new Label(By.XPath(string.Format(_vinWalkReportTableColumnLoc, column)), column.ToString());
        }

        private List<string> GetTextFromVinWalkTableItems(VinWalkTableColumns column)
        {
            var reportElements = new ListElements(By.XPath(string.Format(_vinWalkTableItemsLoc, (int) column)), string.Format("{0} column items", column));

            return reportElements.GetTextFromListElements(TableItemsRegex);
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
                Mileage = mileageValues[i],
                Location = locationValues[i],
                Condition = conditionValues[i],
                Color = colorValues[i],
                Content = contentValues[i],
                CarFax = carFaxValues[i],
                Structural = structuralValues[i],
                TimesRun = timesRunValues[i],
                SalesChanel = salesChanelValues[i],
                Misc = miscValues[i],
                Manual = manualValues[i],
                Floor = floorValues[i],
                Status = statusValues[i],
                SalePrice = salePriceValues[i],
                SoldDate = soldDateValues[i],
                PricingRule = pricingRuleValues[i],
                DatePriced = datePricedValues[i]

            }).ToList();
            return reportItems;
        }

        private List<VinWalkReport> GetVinWalkReportItemsFromFile(FileTypes type)
        {
            var filePath = Directory.GetFiles(Config.DownloadsDir).First();
            var map = CsvUtils.ReadCsv(filePath, true);
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
