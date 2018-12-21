using System;
using Framework.Enums;

namespace Framework.Utils
{
    public class TimeUtils
    {
        public string GetTimeNow(TimeFormats format)
        {
            return DateTime.Now.ToString(format.GetEnumDescription());
        }
    }
}
