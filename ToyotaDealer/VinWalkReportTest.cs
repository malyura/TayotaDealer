using Framework;
using Framework.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyotaDealer.Enums;
using ToyotaDealer.Pages;

namespace ToyotaDealer
{
    [TestClass]
    public class VinWalkReportTest : BaseTest
    {
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
            mainPage.NavigationMenu.ClickNavigationMenuItem(NavigationMenuItems.Reports);

            Logger.Info("Step_3 Select 'VIN walk' report on Reports page");
            var reportsPage = new ReportsPage();
            reportsPage.ClickReportNameLink(ReportNames.VinWalk);
        }

        [TestMethod]
        public void SortYearTest()
        {
            Logger.Info("Step_4 Check sorting by Year on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();
            vinWalkReportPage.ClickVinWalkReportTableColumnElement(VinWalkTableColumns.Year);
            Assert.IsTrue(vinWalkReportPage.IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't active");
            Assert.IsTrue(vinWalkReportPage.IsCorrectSortAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't correct");
        }

        [TestMethod]
        public void ExportReportTest()
        {
            Logger.Info("Step_4 Check export to Csv on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsButton();
            var fileName = vinWalkReportPage.DownloadVinWalkReportFile(FileTypes.Csv);
            vinWalkReportPage.CheckExportToFile(fileName, FileTypes.Csv);
        }
    }
}
