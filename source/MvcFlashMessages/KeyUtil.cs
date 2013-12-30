using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MvcFlashMessages
{
    public static class KeyUtil
    {
        public static string GetKey(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            Regex pattern = new Regex(@"[^\w\-_]");

            string key = pattern.Replace(value, "");
            key = key.ToLower(CultureInfo.InvariantCulture);

            return key;
        }
    }
}
