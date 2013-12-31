using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class FlashMessageTests
    {
        private FlashMessage flashMessage;

        [Test]
        public void Class_must_be_marked_as_serializable()
        {
            var klass = typeof(FlashMessage);
            var attributes = klass.GetCustomAttributes();
            bool isSerializable = attributes.Any(a => a is SerializableAttribute);
            Assert.IsTrue(isSerializable);
        }

        [Test]
        public void Equals_returns_true_when_key_and_message_match()
        {
            flashMessage = CreateFlashMessage();
            var other = CreateFlashMessage();
            Assert.IsTrue(flashMessage.Equals(other));
        }

        [Test]
        [TestCase("success", Result = "success")]
        [TestCase("my-key", Result = "my-key")]
        [TestCase("ERROR", Result = "error")]
        [TestCase("my key", Result = "mykey")]
        public string Key_has_expected_value(string key)
        {
            flashMessage = new FlashMessage(key, "My message");
            return flashMessage.Key;
        }

        [Test]
        public void Key_may_not_be_an_empty_string()
        {
            var ex = Assert.Throws<ArgumentException>(() => new FlashMessage("", "my test message"));
            Debug.WriteLine(ex.Message);
        }

        [Test]
        public void Key_may_not_be_null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new FlashMessage(null, "my test message"));
            Debug.WriteLine(ex.Message);
        }

        [Test]
        public void Message_has_expected_value()
        {
            flashMessage = new FlashMessage("my-key", "My message");
            Assert.AreEqual("My message", flashMessage.Message);
        }

        [Test]
        public void Message_may_not_be_null()
        {
            Assert.Throws<ArgumentNullException>(() => new FlashMessage("test", null));
        }

        [Test]
        public void ToString_returns_expected_value()
        {
            flashMessage = CreateFlashMessage();
            Assert.AreEqual("Hello, World!", flashMessage.ToString());
        }

        private static FlashMessage CreateFlashMessage(string key = "test", string message = "Hello, World!")
        {
            return new FlashMessage(key, message);
        }
    }
}
