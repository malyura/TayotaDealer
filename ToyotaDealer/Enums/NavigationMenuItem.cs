using System.ComponentModel;

namespace ToyotaDealer.Enums
{
    public enum NavigationMenuItem
    {
        [Description("dashboard")]
        Home = 0,
        [Description("controls")]
        Controls,
        [Description("reports")]
        Reports
    }
}
