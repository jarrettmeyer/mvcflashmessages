using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class ControllerExtensions
    {
        public static void Flash(this Controller controller, string key, string message)
        {
            FlashMessageCollection flashMessageCollection = new FlashMessageCollection(controller.TempData);
            FlashMessage flashMessage = new FlashMessage(key, message);
            flashMessageCollection.Add(flashMessage);
        }
    }
}
