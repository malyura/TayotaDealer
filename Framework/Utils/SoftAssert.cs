using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace Framework.Utils
{
    public class SoftAssert
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly List<SingleAssert> _verifications = new List<SingleAssert>();

        public void AreEqual(string expected, string actual, string message)
        {
            _verifications.Add(new SingleAssert(expected, actual, message));
        }

        public void AreEqual(bool expected, bool actual, string message)
        {
            AreEqual(expected.ToString(), actual.ToString(), message);
        }

        public void AreEqual(int expected, int actual, string message)
        {
            AreEqual(expected.ToString(), actual.ToString(), message);
        }

        public void IsTrue(bool actual, string message)
        {
            _verifications.Add(new SingleAssert(true.ToString(), actual.ToString(), message));
        }

        public void IsFalse(bool actual, string message)
        {
            _verifications.Add(new SingleAssert(false.ToString(), actual.ToString(), message));
        }

        public void AssertAll()
        {
            var failed = _verifications.Where(v => v.Failed).ToList();
            failed.ForEach(fail => _logger.Warn(fail.ToString()));
            Assert.IsTrue(failed.Count == 0);
        }

        private class SingleAssert
        {
            private readonly string _message;
            private readonly string _expected;
            private readonly string _actual;

            public bool Failed { get; }

            public SingleAssert(string expected, string actual, string message)
            {
                _message = message;
                _expected = expected;
                _actual = actual;
                Failed = _expected != _actual;

                if (Failed)
                {
                    var screenshotPath = Browser.TakeScreenshot();
                    _message += $". Screenshot captured at: {screenshotPath}";
                }
            }

            public override string ToString()
            {
                return $"'{_message}' assert was expected to be " +
                       $"'{_expected}' but was '{_actual}'";
            }
        }
    }
}
