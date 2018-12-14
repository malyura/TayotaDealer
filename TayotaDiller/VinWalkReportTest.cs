using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TayotaDealer.Enums;
using TayotaDealer.Pages;

namespace TayotaDiller
{
    [TestClass]
    public class VinWalkReportTest : BaseTest
    {
        [TestInitialize]
        public void Precondition()
        {
            logger.Info("Step_1 Login on Login page");
            var loginPage = new LoginPage();
            loginPage.TypeUserName(Config.UserName);
            loginPage.TypePassword(Config.Password);
            loginPage.ClickLogin();

            logger.Info("Step_2 Click menu item 'Reports' on Main page");
            var mainPage = new MainPage();
            mainPage.NavigationMenu.ClickNavigationMenuItem(NavigationMenuItems.Reports);

            logger.Info("Step_3 Select 'VIN walk' report on Reports page");
            var reportsPage = new ReportsPage();
            reportsPage.ClickReportNameLink(ReportNames.VinWalk);
        }

        [TestMethod]
        public void SortYearTest()
        {
            logger.Info("Step_4 Check sorting by Year on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsBtn();
            vinWalkReportPage.ClickVinWalkReportTableColumnLbl(VinWalkTableColumns.Year);
            Assert.IsTrue(vinWalkReportPage.IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't active");
            Assert.IsTrue(vinWalkReportPage.IsCorrectSortAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't correct");
        }

        [TestMethod]
        public void ExportReportTest()
        {
            logger.Info("Step_4 Check export to Csv on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsBtn();
            vinWalkReportPage.DownloadVinWalkReportFile(FileTypes.Csv);
            //Assert.IsTrue(vinWalkReportPage.IsExistsFileWithType(FileTypes.Csv),
            //    $"File type {FileTypes.Csv} not found");
            vinWalkReportPage.CheckReportFile(FileTypes.Csv);
        }
    }
}
