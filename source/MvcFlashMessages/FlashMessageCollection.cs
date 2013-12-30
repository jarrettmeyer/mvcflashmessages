using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(storage != null);
            this.storage = storage;
        }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                EnsureFlashMessagesIsInitialized();
                return flashMessages.Count;
            }
        }

        public static string Key
        {
            get { return key; }
        }

        /// <summary>
        /// Adds a new flash message to the flash message collection with the given key.
        /// </summary>
        /// <param name="key">Flash key.</param>
        /// <param name="message">Flash message.</param>
        public void Add(string key, string message)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(message != null);
            Add(new FlashMessage(key, message));
        }

        /// <summary>
        /// Add a new flash message to the flash message collection.
        /// </summary>
        /// <param name="flashMessage">Flash message.</param>
        public void Add(FlashMessage flashMessage)
        {
            Contract.Requires<ArgumentNullException>(flashMessage != null);
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
                Contract.Requires<ArgumentOutOfRangeException>(0 <= index && index < this.Count);
                EnsureFlashMessagesIsInitialized();
                return flashMessages[index];
            }
        }

        public IEnumerable<FlashMessage> this[string key]
        {
            get
            {
                EnsureFlashMessagesIsInitialized();
                return flashMessages.Where(t => t.Key == key);                
            }
        }

        private void EnsureFlashMessagesIsInitialized()
        {
            // If there are any flash messages already in the collection, do not do this
            // step again, or you will risk populating the collection twice.
            if (flashMessages.Any())
                return;

            IEnumerable<FlashMessage> objectFromStorage = storage.GetFlashMessages();
            Contract.Assert(objectFromStorage != null, "Object from TempDataDictionary cannot be null.");
            flashMessages.AddRange(objectFromStorage);
        }

        private void SaveFlashMessages()
        {
            storage[key] = flashMessages;
            storage.Keep(key);
        }
    }
}
