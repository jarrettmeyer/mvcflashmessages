using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Add a new message to the flash with the given key.
        /// </summary>
        /// <param name="controller">Controller.</param>
        /// <param name="key">Flash key.</param>
        /// <param name="message">Flash message.</param>
        public static void Flash(this Controller controller, string key, string message)
        {
            Contract.Requires<ArgumentNullException>(controller != null);
            Contract.Requires<NullReferenceException>(controller.TempData != null);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(message != null);
            FlashMessageCollection flashMessageCollection = new FlashMessageCollection(controller.TempData);
            FlashMessage flashMessage = new FlashMessage(key, message);
            flashMessageCollection.Add(flashMessage);
        }
    }
}
