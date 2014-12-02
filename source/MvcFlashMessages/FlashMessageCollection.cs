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
        private readonly List<FlashMessage> _flashMessages = new List<FlashMessage>();        
        private readonly TempDataDictionary _storage;
        private static readonly string _key = typeof(FlashMessageCollection).FullName;

        public FlashMessageCollection(TempDataDictionary storage)
        {
            Contract.Requires<ArgumentNullException>(storage != null);
            _storage = storage;
        }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                EnsureFlashMessagesIsInitialized();
                return _flashMessages.Count;
            }
        }

        public static string Key
        {
            get { return _key; }
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
            _flashMessages.Add(flashMessage);
            SaveFlashMessages();
        }

        public IEnumerator<FlashMessage> GetEnumerator()
        {
            try
            {
                EnsureFlashMessagesIsInitialized();
                return _flashMessages.GetEnumerator();
            }
            finally
            {
                _storage.Remove(_key);
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
                Contract.Requires<ArgumentOutOfRangeException>(0 <= index && index < Count);
                EnsureFlashMessagesIsInitialized();
                return _flashMessages[index];
            }
        }

        public IEnumerable<FlashMessage> this[string key]
        {
            get
            {
                EnsureFlashMessagesIsInitialized();
                return _flashMessages.Where(t => t.Key == key);                
            }
        }

        private void EnsureFlashMessagesIsInitialized()
        {
            // If there are any flash messages already in the collection, do not do this
            // step again, or you will risk populating the collection twice.
            if (_flashMessages.Any())
                return;

            IEnumerable<FlashMessage> objectFromStorage = _storage.GetFlashMessages();
            Contract.Assert(objectFromStorage != null, "Object from TempDataDictionary cannot be null.");
            _flashMessages.AddRange(objectFromStorage);
        }

        private void SaveFlashMessages()
        {
            _storage[_key] = _flashMessages;
            _storage.Keep(_key);
        }
    }
}
