using Framework;
using Framework.Elements;
using Framework.Utilities;
using OpenQA.Selenium;
using ToyotaDealer.Enums;

namespace ToyotaDealer.Pages
{
    public class ReportsPage : BasePage
    {
        private static readonly By _reportsPageLocator = By.XPath("//div[normalize-space(@data-module-name)='reports']");
        private readonly string _reportNameLocator = "//table[normalize-space(@class)='table']//a[normalize-space(.)='{0}']";

        public ReportsPage() : base(new Element(_reportsPageLocator, "Reports page"))
        {
        }

        public void ClickReportNameLink(ReportName name)
        {
            GetReportNameLink(name).Click();
        }

        private Element GetReportNameLink(ReportName name)
        {
            return new Element(By.XPath(string.Format(_reportNameLocator, name.GetEnumDescription())), $"Report {name.ToString()}");
        }
    }
}
