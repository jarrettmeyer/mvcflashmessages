﻿using System;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class FlashMessageTests
    {
        private FlashMessage flashMessage;

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
        public void Message_has_expected_value()
        {
            flashMessage = new FlashMessage("my-key", "My message");
            Assert.AreEqual("My message", flashMessage.Message);
        }

        [Test]
        public void Throws_an_exception_when_key_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new FlashMessage(null, "message"));
        }

        [Test]
        public void Throws_an_exception_when_key_is_whitespace()
        {
            Assert.Throws<ArgumentException>(() => new FlashMessage("  ", "message"));
        }
    }
}