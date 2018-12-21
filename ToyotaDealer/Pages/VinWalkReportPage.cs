using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework.Elements;
using OpenQA.Selenium;
using Framework;
using Framework.Enums;
using Framework.Utils;
using HtmlAgilityPack;
using ToyotaDealer.Enums;
using ToyotaDealer.Models;

namespace ToyotaDealer.Pages
{
    public class VinWalkReportPage : BasePage
    {
        private static readonly By _vinWalkReportPageLocator =
            By.XPath($"//*[normalize-space(@class)='location-table-text' and normalize-space(.)='{ReportNames.VinWalk.GetEnumDescription()}']");
        private static readonly string _vinWalkReportTableColumnLocator = "//th[@id='visualization-Tabular-th-{0}']";
        private static readonly string _activeSortVinWalkTableColumnAscendingLocator = 
            "//th[@id='visualization-Tabular-th-{0}' and contains(@class, 'active') and contains(@class, 'asc')]";
        private static readonly string _vinWalkTableItemsLocator = "//div[@id='visualization-Tabular']//tbody//td[{0}]";
        private static readonly string _exportBtnLocator = "export-{0}";
        private static readonly string _lockLocator = _vinWalkReportTableColumnLocator + "//a[@data-lock-column]";
        private static readonly string _lockTableColumnLocator = 
            "//table[@id='lock-table']//th[@id='visualization-Tabular-th-{0}' and contains(@style, 'visible')]";
        private static readonly string _unlockLocator = _lockTableColumnLocator + "//a[@data-lock-column]";
        private readonly Element _updateResultsButton = new Element(By.XPath("//button[contains(@class, 'submit-button') and not (@disabled)]"),
            "Update results");

        public VinWalkReportPage() : base(new Element(_vinWalkReportPageLocator, "VIN walk report page"))
        {
        }

        public bool IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns column)
        {
            return new Element(By.XPath(string.Format(_activeSortVinWalkTableColumnAscendingLocator, column)),
                $"Active sort ascending for {column.ToString()} column").IsPresent();
        }

        public void ClickUpdateResultsButton()
        {
            _updateResultsButton.Click();
        }

        public void ClickVinWalkReportTableColumnElement(VinWalkTableColumns column)
        {
            GetVinWalkReportTableColumnElement(column).Click();
        }

        public void ClickLockColumnsElements(List<VinWalkTableColumns> columns)
        {
            foreach (var element in columns)
            {
                GetLockElement(element).MoveToElement();
                GetLockElement(element).ClickJs();
            }
        }

        public void ClickUnlockColumnsElements(List<VinWalkTableColumns> columns)
        {
            foreach (var element in columns)
            {
                GetUnlockElement(element).Click();
            }
        }

        public bool IsColumnLocked(VinWalkTableColumns column)
        {
            return new Element(By.XPath(string.Format(_lockTableColumnLocator, column.GetEnumDescription())),
                $"Locked column {column}").IsPresent(double.Parse(ElementTimeout.Small.GetEnumDescription()));
        }

        public bool IsCorrectColumnYearSortAscending()
        {
            var vinValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Vin);
            var yearValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Year);
            var reportItems = vinValues.Select((t, i) => new VinWalkReport
            {
                Vin = t,
                Year = int.Parse(yearValues[i]),
            }).ToList();

            for (var i = 0; i < reportItems.Count - 1; i++)
            {
                if (reportItems[i].Year > reportItems[i + 1].Year)
                {
                    Logger.Warn($"VIN Walk report item with VIN value {reportItems[i].Vin} incorrectly sorted");

                    return false;
                }
            }

            return true;
        }

        public string DownloadVinWalkReportFile(FileTypes fileType)
        {
            FileUtils.DeleteDirectoryIfExists(Config.DownloadsDirectory);
            FileUtils.CreateDirectoryIfNotExists(Config.DownloadsDirectory);
            new Element(By.Id(string.Format(_exportBtnLocator, fileType.ToString())), $"{fileType} export").Click();
            Wait.Until(result => FileUtils.IsOverDownloadFile(Config.DownloadsDirectory,
                Directory.GetFiles(Config.DownloadsDirectory).FirstOrDefault(),
                fileType.GetEnumDescription()));

            return Directory.GetFiles(Config.DownloadsDirectory).First();

        }

        public List<VinWalkTableColumns> GetRandomVinWalkReportTableColumns(int numberOfElements)
        {
            var allColumnNameList = Enum.GetValues(typeof(VinWalkTableColumns)).Cast<VinWalkTableColumns>().ToList();
            var randomColumnNameList = new List<VinWalkTableColumns>();

            for (var i = 0; i < numberOfElements; i++)
            {
                var number = RandomUtils.GetRandomNumber(allColumnNameList.Count);
                randomColumnNameList.Add(allColumnNameList[number]);
                allColumnNameList.RemoveAt(number);
            }

            return randomColumnNameList;
        }

        public List<VinWalkReport> GetVinWalkReportItemsFromTable()
        {
            var vinValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Vin);
            var yearValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Year);
            var makeValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Make);
            var modelValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Model);
            var trimValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Trim);
            var mmrValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Mmr);
            var mileageValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Mileage);
            var locationValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Location);
            var conditionValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Condition);
            var colorValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Color);
            var contentValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Content);
            var carFaxValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.CarFax);
            var structuralValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Structural);
            var timesRunValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.TimesRun);
            var salesChanelValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SalesChanel);
            var miscValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Misc);
            var manualValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Manual);
            var floorValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Floor);
            var statusValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.Status);
            var salePriceValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SalePrice);
            var soldDateValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.SoldDate);
            var pricingRuleValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.PricingRule);
            var datePricedValues = GetTextFromVinWalkTableItems(VinWalkTableColumns.DatePriced);

            var reportItems = vinValues.Select((t, i) => new VinWalkReport
            {
                Vin = t,
                Year = int.Parse(yearValues[i]),
                Make = makeValues[i],
                Model = modelValues[i],
                Trim = trimValues[i],
                Mmr = mmrValues[i],
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

        private static Element GetLockElement(VinWalkTableColumns column)
        {
            return new Element(By.XPath(string.Format(_lockLocator, column.GetEnumDescription())), $"Lock column {column}");
        }

        private static Element GetUnlockElement(VinWalkTableColumns column)
        {
            return new Element(By.XPath(string.Format(_unlockLocator, column.GetEnumDescription())), $"Unlock column {column}");
        }

        private static Element GetVinWalkReportTableColumnElement(VinWalkTableColumns column)
        {
            return new Element(By.XPath(string.Format(_vinWalkReportTableColumnLocator, column.GetEnumDescription())), column.ToString());
        }

        private static List<string> GetTextFromVinWalkTableItems(VinWalkTableColumns column)
        {
            var pageSource = Browser.GetPageSource();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(pageSource);
            var textFromVinWalkTableItems = htmlDoc.DocumentNode.
                SelectNodes(string.Format(_vinWalkTableItemsLocator, (int)column)).
                Select(htmlNode => htmlNode.InnerText).ToList();

            return textFromVinWalkTableItems;
        }
    }
}
