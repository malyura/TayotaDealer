using System.ComponentModel;

namespace Framework.Enums
{
    public enum ElementTimeout
    {
        [Description("0")]
        Null,
        [Description("5")]
        Small,
        [Description("10")]
        Middle,
        [Description("40")]
        Long
    }
}
