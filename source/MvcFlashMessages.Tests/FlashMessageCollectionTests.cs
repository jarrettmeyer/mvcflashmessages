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

        [Test]
        public void Key_is_a_public_property()
        {
            Assert.AreEqual("MvcFlashMessages.FlashMessageCollection", FlashMessageCollection.Key);
        }

        [Test]
        public void String_indexer_can_get_messages_with_given_key()
        {
            collection.Add(new FlashMessage("info", "Test info message."));
            collection.Add(new FlashMessage("info", "Test info message 2."));
            collection.Add(new FlashMessage("warning", "Test warning message."));

            var messages = collection["info"];

            Assert.AreEqual(2, messages.Count());
        }

        [Test]
        public void String_indexer_can_return_an_empty_collection()
        {
            var messages = collection["test"];
            Assert.IsNotNull(messages);
            Assert.AreEqual(0, messages.Count());
        }
    }
}
