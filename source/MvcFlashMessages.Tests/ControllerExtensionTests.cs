using System;
using System.Web.Mvc;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class ControllerExtensionTests
    {
        private Controller controller;
        private FlashMessageCollection flashMessageCollection;

        [SetUp]
        public void Before_each_test()
        {
            controller = new FakeController();
            flashMessageCollection = new FlashMessageCollection(controller.TempData);
        }

        [Test]
        public void Can_add_a_flash_message()
        {
            controller.Flash("success", "Look at me!");

            Assert.AreEqual(1, flashMessageCollection.Count);
            Assert.AreEqual("success", flashMessageCollection[0].Key);
            Assert.AreEqual("Look at me!", flashMessageCollection[0].Message);
        }

        [Test]
        public void Can_add_multiple_flash_messages_with_the_same_key()
        {
            controller.Flash("alert", "Test 1");
            controller.Flash("alert", "Test 2");
            Assert.AreEqual(2, flashMessageCollection.Count);
            Assert.AreEqual("alert", flashMessageCollection[0].Key);
            Assert.AreEqual("alert", flashMessageCollection[1].Key);
        }

        [Test]
        public void Controller_cannot_be_null()
        {
            Controller nullController = null;
            Assert.Throws<ArgumentNullException>(() => nullController.Flash("test", "This is a test."));
        }

        [Test]
        public void Default_controller_has_no_flash_messages()
        {
            Assert.AreEqual(0, flashMessageCollection.Count);
        }

        [Test]
        public void It_throws_an_exception_if_the_key_is_an_empty_string()
        {
            Assert.Throws<ArgumentException>(() => controller.Flash("", "Boom!"));
        }

        [Test]
        public void It_throws_an_exception_if_the_key_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => controller.Flash(null, "Boom!"));
        }
    }
}
