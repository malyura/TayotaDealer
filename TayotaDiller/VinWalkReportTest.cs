using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework;
using TayotaDiller.Enums;
using TayotaDiller.Pages;

namespace TayotaDiller
{
    [TestClass]
    public class VinWalkReportTest : BaseTest
    {
        [TestMethod]
        public void RunTest()
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

            logger.Info("Step_4 Check sorting by Year on Vin Walk Report page");
            var vinWalkReportPage = new VinWalkReportPage();
            vinWalkReportPage.ClickUpdateResultsBtn();
            vinWalkReportPage.ClickVinWalkReportTableColumnLbl(VinWalkTableColumns.Year);
            Assert.IsTrue(vinWalkReportPage.IsActiveSortVinWalkTableColumnAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't active");
            Assert.IsTrue(vinWalkReportPage.IsCorrectSortAscending(VinWalkTableColumns.Year),
                "Sort ascending for column 'Year' isn't correct");
        }
    }
}
