using System.ComponentModel;

namespace ToyotaDealer.Enums
{
    public enum ReportName
    {
        [Description("Performance Reporting By Time6")]
        PerformanceReportingByTime = 0,
        [Description("VIN Detail")]
        VinDetail,
        [Description("VIN Walk")]
        VinWalk,
        [Description("Performance Reporting By Location")]
        PerformanceReportingByLocation
    }
}
