using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class TempDataDictionaryExtensions
    {
        public static IEnumerable<FlashMessage> GetFlashMessages(this TempDataDictionary tempData)
        {
            if (tempData == null)
                throw new ArgumentNullException("tempData");

            object obj = tempData[FlashMessageCollection.Key];
            if (obj != null)
            {
                return (IList<FlashMessage>)obj;
            }
            IEnumerable<FlashMessage> flashMessages = (obj != null) ? (IEnumerable<FlashMessage>)obj : new List<FlashMessage>();
            return flashMessages;
        }
    }
}
