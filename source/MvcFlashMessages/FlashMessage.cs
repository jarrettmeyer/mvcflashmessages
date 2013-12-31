using System;
using System.Diagnostics.Contracts;

namespace MvcFlashMessages
{
    [Serializable]
    public class FlashMessage : IEquatable<FlashMessage>
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

        public bool Equals(FlashMessage other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Key.Equals(other.Key, StringComparison.InvariantCulture) &&
                   Message.Equals(other.Message, StringComparison.InvariantCulture);
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
