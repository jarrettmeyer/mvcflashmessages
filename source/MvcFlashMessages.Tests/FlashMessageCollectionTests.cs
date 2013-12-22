using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class FlashMessageCollectionTests
    {
        private FlashMessageCollection collection;
        private TempDataDictionary tempData;

        [SetUp]
        public void Before_each_test()
        {
            tempData = new TempDataDictionary();
            collection = new FlashMessageCollection(tempData);
        }

        [Test]
        public void Adding_a_message_should_add_key_to_temp_data()
        {
            collection.Add(new FlashMessage("info", "Hello, World!"));
            var key = tempData.Keys.First();
            Assert.AreEqual("MvcFlashMessages.FlashMessageCollection", key);
        }

        [Test]
        public void Can_get_an_empty_collection()
        {
            var messages = collection.ToList();
            Assert.AreEqual(0, messages.Count());
        }

        [Test]
        public void Can_iterate_a_collection_of_messages()
        {
            collection.Add(new FlashMessage("info", "Hello, World!"));
            var messages = collection.ToList();
            Assert.AreEqual(1, messages.Count);
        }

        [Test]
        public void Iterating_messages_should_remove_key()
        {
            collection.Add(new FlashMessage("info", "Hello, World!"));
            var messages = collection.ToList();
            Assert.AreEqual(0, tempData.Keys.Count());
        }
    }
}
