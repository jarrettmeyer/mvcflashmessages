using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public class FlashMessageCollection : IEnumerable<FlashMessage>
    {
        private IList<FlashMessage> flashMessages; 
        private readonly TempDataDictionary storage;
        private static readonly string key = typeof(FlashMessageCollection).FullName;

        public FlashMessageCollection(TempDataDictionary storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            this.storage = storage;
        }

        public void Add(FlashMessage flashMessage)
        {
            EnsureFlashMessagesIsInitialized();
            flashMessages.Add(flashMessage);
            SaveFlashMessages();
        }

        public IEnumerator<FlashMessage> GetEnumerator()
        {
            EnsureFlashMessagesIsInitialized();
            return flashMessages.GetEnumerator();
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
            object objectFromStorage = storage[key];
            flashMessages = (objectFromStorage != null) ? (IList<FlashMessage>)objectFromStorage : new List<FlashMessage>();
        }

        private void SaveFlashMessages()
        {
            storage[key] = flashMessages;
            storage.Keep(key);
        }
    }
}
