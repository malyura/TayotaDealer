using System;
using Framework.Enums;

namespace Framework.Utilities
{
    public class TimeUtilities
    {
        public string GetTimeNow(TimeFormat format)
        {
            return DateTime.Now.ToString(format.GetEnumDescription());
        }
    }
}
