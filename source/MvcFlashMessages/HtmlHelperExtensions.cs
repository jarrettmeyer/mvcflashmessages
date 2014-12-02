using System;
using System.Diagnostics.Contracts;
using System.Web;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class HtmlHelperExtensions
    {
        private readonly static string _closeTag = string.Format("<span class=\"close\" onclick=\"javascript:{0}\">&times;</span>", Config.Instance.CloseClickEvent);

        /// <summary>
        /// Renders the HTML containing the flash messages.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns>HTML string</returns>
        public static IHtmlString RenderFlash(this HtmlHelper htmlHelper)
        {
            Contract.Requires<ArgumentNullException>(htmlHelper != null);
            var outerDiv = CreateOuterDiv();
            FlashMessageCollection flashMessageCollection = new FlashMessageCollection(htmlHelper.ViewContext.TempData);
            foreach (FlashMessage flashMessage in flashMessageCollection)
            {
                AddFlashMessageToOuterDiv(flashMessage, outerDiv);
            }
            return MvcHtmlString.Create(outerDiv.ToString(TagRenderMode.Normal));
        }

        private static void AddCssClasses(TagBuilder innerDiv, FlashMessage flashMessage)
        {
            innerDiv.AddCssClass(Config.Instance.InnerCssClass + "-" + flashMessage.Key);
            innerDiv.AddCssClass(Config.Instance.InnerCssClass);
        }

        private static string AddCloseTag()
        {
            return Config.Instance.IsClosable ? _closeTag : "";
        }

        private static void AddFlashMessageToOuterDiv(FlashMessage flashMessage, TagBuilder outerDiv)
        {
            TagBuilder innerDiv = new TagBuilder("div");
            AddCssClasses(innerDiv, flashMessage);
            var innerDivText = GetInnerDivContent(innerDiv, flashMessage);
            outerDiv.InnerHtml += innerDivText;
        }

        private static TagBuilder CreateOuterDiv()
        {
            TagBuilder outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass(Config.Instance.OuterCssClass);
            return outerDiv;
        }

        private static string GetInnerDivContent(TagBuilder innerDiv, FlashMessage flashMessage)
        {
            innerDiv.InnerHtml = "";
            innerDiv.InnerHtml += AddCloseTag();
            innerDiv.InnerHtml += flashMessage.Message;
            var innerDivText = innerDiv.ToString(TagRenderMode.Normal);
            return innerDivText;
        }
    }
}
