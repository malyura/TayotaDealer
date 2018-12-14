using System.Text.RegularExpressions;

namespace Framework.Utils
{
    public class StringUtils
    {
        public static string GetMatch(string text, string pattern)
        {
            return Regex.Match(text, pattern).Value;
        }
    }
}
