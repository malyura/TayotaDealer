using System;
using System.ComponentModel;

namespace Framework.Utils
{
    /// <summary>
    /// To work with the data type enum
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// get string description from enum
        /// </summary>
        /// <param name="value">enum value</param>
        /// <returns>enum string description</returns>
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}
