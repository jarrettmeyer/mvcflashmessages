using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public class FlashMessageCollection : IEnumerable<FlashMessage>
    {
        private readonly List<FlashMessage> flashMessages = new List<FlashMessage>();        
        private readonly TempDataDictionary storage;
        private static readonly string key = typeof(FlashMessageCollection).FullName;

        public FlashMessageCollection(TempDataDictionary storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            this.storage = storage;
        }

        public int Count
        {
            get
            {
                EnsureFlashMessagesIsInitialized();
                return flashMessages.Count;
            }
        }

        public static string Key
        {
            get { return key; }
        }

        public void Add(FlashMessage flashMessage)
        {
            EnsureFlashMessagesIsInitialized();
            flashMessages.Add(flashMessage);
            SaveFlashMessages();
        }

        public IEnumerator<FlashMessage> GetEnumerator()
        {
            try
            {
                EnsureFlashMessagesIsInitialized();
                return flashMessages.GetEnumerator();
            }
            finally
            {
                storage.Remove(key);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public FlashMessage this[int index]
        {
            get
            {
                EnsureFlashMessagesIsInitialized();
                return flashMessages[index];
            }
        }

        private void EnsureFlashMessagesIsInitialized()
        {
            // If there are any flash messages already in the collection, do not do this
            // step again, or you will risk populating the collection twice.
            if (flashMessages.Any())
                return;

            IEnumerable<FlashMessage> objectFromStorage = storage.GetFlashMessages();
            flashMessages.AddRange(objectFromStorage);
        }

        private void SaveFlashMessages()
        {
            storage[key] = flashMessages;
            storage.Keep(key);
        }
    }
}
