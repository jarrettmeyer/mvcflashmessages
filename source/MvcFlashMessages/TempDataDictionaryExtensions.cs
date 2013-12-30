using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class TempDataDictionaryExtensions
    {
        public static IEnumerable<FlashMessage> GetFlashMessages(this TempDataDictionary tempData)
        {
            Contract.Requires<ArgumentNullException>(tempData != null);
            Contract.Ensures(Contract.Result<IEnumerable<FlashMessage>>() != null);

            object obj = tempData[FlashMessageCollection.Key];
            IEnumerable<FlashMessage> flashMessages = (obj as IEnumerable<FlashMessage>) ?? new List<FlashMessage>();
            return flashMessages;
        }
    }
}
