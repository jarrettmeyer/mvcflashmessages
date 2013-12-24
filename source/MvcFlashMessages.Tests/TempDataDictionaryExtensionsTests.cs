using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class TempDataDictionaryExtensionsTests
    {
        [Test]
        public void GetFlashMessages_returns_a_collection()
        {
            TempDataDictionary tempData = new TempDataDictionary();
            var original = new FlashMessageCollection(tempData);
            original.Add(new FlashMessage("info", "Hello, World!"));

            var flashMessages = tempData.GetFlashMessages();
            Assert.AreEqual(1, flashMessages.Count());
        }

        [Test]
        public void GetFlashMessages_returns_a_new_collection_when_not_assigned()
        {
            TempDataDictionary tempData = new TempDataDictionary();
            var flashMessages = tempData.GetFlashMessages();
            Assert.AreEqual(0, flashMessages.Count());
        }

        [Test]
        public void GetFlashMessages_throws_an_exception_when_temp_data_is_null()
        {
            TempDataDictionary tempData = null;
            Assert.Throws<ArgumentNullException>(() => tempData.GetFlashMessages());
        }
    }
}
