using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MvcFlashMessages
{
    public static class KeyUtil
    {
        public static string GetKey(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value", "Value cannot be null.");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null, empty, or white space.", "value");

            Regex pattern = new Regex(@"[^\w\-_]");

            string key = pattern.Replace(value, "");
            key = key.ToLower(CultureInfo.InvariantCulture);

            return key;
        }
    }
}
