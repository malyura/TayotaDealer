using System.Collections.Generic;
using System.Linq;
using Framework.Elements;
using OpenQA.Selenium;
using TayotaDiller.Enums;
using Framework;
using Framework.Utils;
using TayotaDiller.Models;

namespace TayotaDiller.Pages
{
    public class VinWalkReportPage : BasePage
    {
        private static readonly By _vinWalkReportPageLoc =
            By.XPath(string.Format("//*[normalize-space(@class)='location-table-text' and normalize-space(.)='{0}']", ReportNames.VinWalk.GetEnumDescription()));
        private readonly string _vinWalkReportTableColumnLoc = "//th[@id='visualization-Tabular-th-{0}']";
        private readonly string _activeSortVinWalkTableColumnAscendingLoc = 
            "//th[@id='visualization-Tabular-th-{0}' and contains(@class, 'active') and contains(@class, 'asc')]";
        private readonly string _vinWalkTableItemsLoc = "//div[@id='visualization-Tabular']//tbody//td[{0}]";
        private readonly Button _updateResultsBtn = new Button(By.XPath("//button[contains(@class, 'submit-button') and not (@disabled)]"),
            "Update results");

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

        private Label GetVinWalkReportTableColumnLbl(VinWalkTableColumns column)
        {
            return new Label(By.XPath(string.Format(_vinWalkReportTableColumnLoc, column)), column.ToString());
        }

        private ListElements GetItemsFromVinWalkTableColumn(VinWalkTableColumns column)
        {
            return new ListElements(By.XPath(string.Format(_vinWalkTableItemsLoc, (int) column)), string.Format("{0} column items", column));
        }

        private List<VinWalkReport> GetVinWalkReportItems()
        {
            var vinValues = GetItemsFromVinWalkTableColumn(VinWalkTableColumns.VIN).GetTextFromListElements();
            var yearValues = GetItemsFromVinWalkTableColumn(VinWalkTableColumns.Year).GetTextFromListElements();
            return vinValues.Select((t, i) => new VinWalkReport {VIN = t, Year = int.Parse(yearValues[i])}).ToList();
        }
    }
}
