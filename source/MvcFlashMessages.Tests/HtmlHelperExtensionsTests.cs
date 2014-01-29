using System;
using System.Diagnostics;
using System.Web.Mvc;
using NUnit.Framework;

namespace MvcFlashMessages
{
    [TestFixture]
    public class HtmlHelperExtensionsTests
    {
        private string flash; 
        private FlashMessageCollection flashMessageCollection;
        private HtmlHelper htmlHelper;

        [SetUp]
        public void Before_each_test()
        {
            ViewPage viewPage = new ViewPage();
            ViewContext viewContext = new ViewContext();
            viewContext.TempData = new TempDataDictionary();
            
            htmlHelper = new HtmlHelper(viewContext, viewPage);
            flashMessageCollection = new FlashMessageCollection(viewContext.TempData);
        }

        [TearDown]
        public void After_each_test()
        {
            Config.Instance.IsClosable = false;
        }

        [Test]
        public void Adds_javascript_click_event_when_closable()
        {
            Config.Instance.IsClosable = true;
            flashMessageCollection.Add("test", "Hello, World!");
            flash = htmlHelper.RenderFlash().ToHtmlString();
            StringAssert.IsMatch("onclick=\"javascript:\\(function\\(.+\\){.+}\\)\\(this\\);\"", flash);
        }

        [Test]
        public void Can_display_an_empty_div()
        {
            flash = htmlHelper.RenderFlash().ToHtmlString();
            Assert.AreEqual("<div class=\"flash-messages\"></div>", flash);
        }

        [Test]
        public void Can_display_multiple_flash_messages()
        {
            flashMessageCollection.Add(new FlashMessage("one", "One!"));
            flashMessageCollection.Add(new FlashMessage("two", "Two!"));
            flash = htmlHelper.RenderFlash().ToHtmlString();
            Assert.AreEqual("<div class=\"flash-messages\"><div class=\"flash-message flash-message-one\">One!</div><div class=\"flash-message flash-message-two\">Two!</div></div>", flash);
        }

        [Test]
        public void Can_display_one_flash_message()
        {
            flashMessageCollection.Add(new FlashMessage("error", "Something broke!"));
            flash = htmlHelper.RenderFlash().ToHtmlString();
            Assert.AreEqual("<div class=\"flash-messages\"><div class=\"flash-message flash-message-error\">Something broke!</div></div>", flash);
        }

        [Test]
        public void HtmlHelper_cannot_be_null()
        {
            htmlHelper = null;
            var ex = Assert.Throws<ArgumentNullException>(() => htmlHelper.RenderFlash());
            Debug.WriteLine(ex);
        }
    }
}
