using System.Collections.Generic;
using Framework.Utils;
using ToyotaDealer.Models;

namespace ToyotaDealer.Helpers
{
    public class AssertsHelper
    {
        private readonly SoftAssert _softAssert = new SoftAssert();

        public void AssertAreEqualVinWalkReports(List<VinWalkReport> expectedReports, List<VinWalkReport> actualReports, string message)
        {
            for (var i = 0; i < expectedReports.Count; i++)
            {
                _softAssert.AreEqual(expectedReports[i].Vin, actualReports[i].Vin, message);
                _softAssert.AreEqual(expectedReports[i].Year.ToString(), actualReports[i].Year.ToString(), message);
                _softAssert.AreEqual(expectedReports[i].Make, actualReports[i].Make, message);
                _softAssert.AreEqual(expectedReports[i].Model, actualReports[i].Model, message);
                _softAssert.AreEqual(expectedReports[i].Trim, actualReports[i].Trim, message);
                _softAssert.AreEqual(expectedReports[i].Mmr, actualReports[i].Mmr, message);
                _softAssert.AreEqual(expectedReports[i].Mileage, actualReports[i].Mileage, message);
                _softAssert.AreEqual(expectedReports[i].Location, actualReports[i].Location, message);
                _softAssert.AreEqual(expectedReports[i].Condition, actualReports[i].Condition, message);
                _softAssert.AreEqual(expectedReports[i].Color, actualReports[i].Color, message);
                _softAssert.AreEqual(expectedReports[i].Content, actualReports[i].Content, message);
                _softAssert.AreEqual(expectedReports[i].CarFax, actualReports[i].CarFax, message);
                _softAssert.AreEqual(expectedReports[i].Structural, actualReports[i].Structural, message);
                _softAssert.AreEqual(expectedReports[i].TimesRun, actualReports[i].TimesRun, message);
                _softAssert.AreEqual(expectedReports[i].SalesChanel, actualReports[i].SalesChanel, message);
                _softAssert.AreEqual(expectedReports[i].Misc, actualReports[i].Misc, message);
                _softAssert.AreEqual(expectedReports[i].Manual, actualReports[i].Manual, message);
                _softAssert.AreEqual(expectedReports[i].Floor, actualReports[i].Floor, message);
                _softAssert.AreEqual(expectedReports[i].Status, actualReports[i].Status, message);
                _softAssert.AreEqual(expectedReports[i].SalePrice, actualReports[i].SalePrice, message);
                _softAssert.AreEqual(expectedReports[i].SoldDate, actualReports[i].SoldDate, message);
                _softAssert.AreEqual(expectedReports[i].PricingRule, actualReports[i].PricingRule, message);
                _softAssert.AreEqual(expectedReports[i].DatePriced, actualReports[i].DatePriced, message);
            }

            _softAssert.AssertAll();
        }
    }
}
