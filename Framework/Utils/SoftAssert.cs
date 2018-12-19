using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace Framework.Utils
{
    public class SoftAssert
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static void AreEqual(object expected, object actual, string message)
        {
            try
            {
                Assert.AreEqual(expected, actual);
            }
            catch (UnitTestAssertException exception)
            {
                _logger.Warn(message + "\n" + exception.Message);
            }
        }

        public static void IsTrue(bool condition, string message)
        {
            try
            {
                Assert.IsTrue(condition);
            }
            catch (UnitTestAssertException exception)
            {
                _logger.Warn(message + "\n" + exception.Message);
            }
        }
    }
}
