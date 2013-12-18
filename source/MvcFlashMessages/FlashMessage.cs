namespace MvcFlashMessages
{
    public class FlashMessage
    {
        private readonly string key;
        private readonly string message;

        public FlashMessage(string key, string message)
        {
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
