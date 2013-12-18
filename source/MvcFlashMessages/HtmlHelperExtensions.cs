using System.Web;
using System.Web.Mvc;

namespace MvcFlashMessages
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderFlash(this HtmlHelper htmlHelper)
        {
            TagBuilder outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass(Config.Instance.OuterCssClass);

            FlashMessageCollection flashMessageCollection = new FlashMessageCollection(htmlHelper.ViewContext.TempData);
            foreach (FlashMessage flashMessage in flashMessageCollection)
            {
                TagBuilder innerDiv = new TagBuilder("div");
                innerDiv.AddCssClass(Config.Instance.InnerCssClass + "-" + flashMessage.Key); 
                innerDiv.AddCssClass(Config.Instance.InnerCssClass);
                innerDiv.InnerHtml = flashMessage.Message;
                outerDiv.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(outerDiv.ToString(TagRenderMode.Normal));
        }
    }
}
