using System;
using System.Diagnostics.Contracts;

namespace MvcFlashMessages
{
    [Serializable]
    public class FlashMessage
    {
        private readonly string key;
        private readonly string message;

        public FlashMessage(string key, string message)
        {
            Contract.Requires<ArgumentNullException>(key != null, "Key cannot be null.");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(key), "Key cannot be null, empty string, or white space.");
            Contract.Requires<ArgumentNullException>(message != null, "Message cannot be null.");

            this.key = KeyUtil.GetKey(key);
            this.message = message;
        }

        public string Key
        {
            get { return key; }
        }

        public string Message
        {
            get { return message; }
        }
    }
}
