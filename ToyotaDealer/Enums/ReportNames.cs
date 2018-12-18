using System.ComponentModel;

namespace ToyotaDealer.Enums
{
    public enum ReportNames
    {
        [Description("Performance Reporting By Time6")]
        PerformanceReportingByTime,
        [Description("VIN Detail")]
        VinDetail,
        [Description("VIN Walk")]
        VinWalk,
        [Description("Performance Reporting By Location")]
        PerformanceReportingByLocation
    }
}
