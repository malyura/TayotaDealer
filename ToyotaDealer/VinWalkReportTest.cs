using System.IO;
using Framework;
using Framework.Enums;
using Framework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyotaDealer.Enums;
using ToyotaDealer.Helpers;
using ToyotaDealer.Pages;

namespace ToyotaDealer
{
    [TestClass]
    public class VinWalkReportTest : BaseTest
    {
        private readonly SoftAssert _softAssert = new SoftAssert();

        [TestInitialize]
        public void Precondition()
        {
            Logger.Info("Step_1 Login on Login page");
            var loginPage = new LoginPage();
            loginPage.TypeUserName(Config.UserName);
            loginPage.TypePassword(Config.Password);
            loginPage.ClickLogin();

            Logger.Info("Step_2 Click menu item 'Reports' on Main page");
            var mainPage = new MainPage();
            mainPage.NavigationMenu.ClickNavigationMenuItem(NavigationMenuItem.Reports);

            Logger.Info("Step_3 Select 'VIN walk' report on Reports page");
            var reportsPage = new ReportsPage();
            reportsPage.ClickReportNameLink(ReportName.VinWalk);
        }

        [TestMethod]
        public void SortYearTest()
        {
            Logger.Info("Step_4 Check sorting by Year on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();
            vinWalkReportPage.ClickVinWalkReportTableColumnElement(VinWalkTableColumn.Year);
            Assert.IsTrue(vinWalkReportPage.IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumn.Year),
                "Sort ascending for column 'Year' isn't active");
            Assert.IsTrue(vinWalkReportPage.IsCorrectColumnYearSortAscending(),
                "Sort ascending for column 'Year' isn't correct");
        }

        [TestMethod]
        public void ExportReportToCsvTest()
        {
            Logger.Info("Step_4 Check export to Csv on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();

            var fileName = vinWalkReportPage.DownloadVinWalkReportFile(FileType.Csv);
            Assert.IsTrue(File.Exists(fileName), $"File {fileName} doesn't exist");

            var vinWalkReportItemsFromTable = vinWalkReportPage.GetVinWalkReportItemsFromTable();
            Assert.IsNotNull(vinWalkReportItemsFromTable, "Report from VIN Walk report table has value null");

            var vinWalkReportItemsFromFile = FileReadingHelper.GetVinWalkReportItemsFromFile(fileName, FileType.Csv);
            Assert.IsNotNull(vinWalkReportItemsFromFile, "Report from csv file with VIN Walk report has value null");

            var assertHelper = new AssertsHelper();
            assertHelper.AssertAreEqualVinWalkReports(vinWalkReportItemsFromTable, vinWalkReportItemsFromTable,
                "Items from VIN Walk report table and VIN Walk report csv file aren't equal");
        }

        [TestMethod]
        public void ExportReportToExcelTest()
        {
            Logger.Info("Step_4 Check export to Csv on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();

            var fileName = vinWalkReportPage.DownloadVinWalkReportFile(FileType.Excel);
            Assert.IsTrue(File.Exists(fileName), $"File {fileName} doesn't exist");

            var vinWalkReportItemsFromTable = vinWalkReportPage.GetVinWalkReportItemsFromTable();
            Assert.IsNotNull(vinWalkReportItemsFromTable, "Report from VIN Walk report table has value null");

            var vinWalkReportItemsFromFile = FileReadingHelper.GetVinWalkReportItemsFromFile(fileName, FileType.Excel);
            Assert.IsNotNull(vinWalkReportItemsFromFile, "Report from excel file with VIN Walk report has value null");

            var assertHelper = new AssertsHelper();
            assertHelper.AssertAreEqualVinWalkReports(vinWalkReportItemsFromTable, vinWalkReportItemsFromTable,
                "Items from VIN Walk report table and VIN Walk report excel file aren't equal");
        }

        [TestMethod]
        public void LocksTest()
        {
            const int columnsNumberToCheck = 3;
            Logger.Info("Step_4 Check table column lock on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();
            var randomVinWalkReportColumns = vinWalkReportPage.GetRandomVinWalkReportTableColumns(columnsNumberToCheck);
            vinWalkReportPage.ClickLockColumnsElements(randomVinWalkReportColumns);
            randomVinWalkReportColumns.ForEach(column => _softAssert.IsTrue(vinWalkReportPage.IsColumnLocked(column),
                $"Column {column} isn't locked"));

            vinWalkReportPage.ClickUnlockColumnsElements(randomVinWalkReportColumns);
            randomVinWalkReportColumns.ForEach(column => _softAssert.IsFalse(vinWalkReportPage.IsColumnLocked(column),
                $"Column {column} isn't unlocked"));

            _softAssert.AssertAll();
        }
    }
}
