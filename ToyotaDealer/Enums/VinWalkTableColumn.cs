using System.ComponentModel;

namespace ToyotaDealer.Enums
{
    public enum VinWalkTableColumn
    {
        [Description("VIN")]
        Vin = 1,
        [Description("Year")]
        Year,
        [Description("Make")]
        Make,
        [Description("Model")]
        Model,
        [Description("Trim")]
        Trim,
        [Description("MMR")]
        Mmr,
        [Description("Mileage")]
        Mileage,
        [Description("Location")]
        Location,
        [Description("Condition")]
        Condition,
        [Description("Color")]
        Color,
        [Description("Content")]
        Content,
        [Description("CarFax")]
        CarFax,
        [Description("Structural")]
        Structural,
        [Description("Times Run")]
        TimesRun,
        [Description("Sales Channel")]
        SalesChanel,
        [Description("Misc.")]
        Misc,
        [Description("Manual")]
        Manual,
        [Description("Floor")]
        Floor,
        [Description("Status")]
        Status,
        [Description("Sale Price")]
        SalePrice,
        [Description("Sold Date")]
        SoldDate,
        [Description("Pricing Rule")]
        PricingRule,
        [Description("Date Priced")]
        DatePriced
    }
}
