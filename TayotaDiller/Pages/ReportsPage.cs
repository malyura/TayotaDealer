using Framework;
using Framework.Elements;
using Framework.Utils;
using OpenQA.Selenium;
using TayotaDiller.Menus;

namespace TayotaDiller.Pages
{
    public class ReportsPage : BasePage
    {
        private static By _reportsPageLoc = By.XPath("//div[normalize-space(@data-module-name)='reports']");
        private readonly string _reportNameLoc = "//table[normalize-space(@class)='table']//a[normalize-space(.)='{0}']";

        public ReportsPage() : base(new Label(_reportsPageLoc, "Reports page"))
        {
        }

        public void ClickReportNameLink(ReportNames name)
        {
            GetReportNameLink(name).Click();
        }

        private Link GetReportNameLink(ReportNames name)
        {
            return new Link(By.XPath(string.Format(_reportNameLoc, name.GetEnumDescription())), string.Format("Report {0}", name.ToString()));
        }
    }
}
