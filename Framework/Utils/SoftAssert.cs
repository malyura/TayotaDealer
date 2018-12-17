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
    }
}
