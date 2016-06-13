using System;
using System.Web.Mvc;
using CC.UI.Webhost.Models;

namespace CC.UI.Webhost.Infrastructure
{
    public static class HtmlExtensionHelpers
    {
        public static MvcHtmlString ImageTag(this HtmlHelper html, string src, string alt, int? height = null, int? width = null,
                                             string title = null, string cssClass = null)
        {
            if (string.IsNullOrWhiteSpace(src))
                return null;
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", url.Content(src));
            imageTagBuilder.MergeAttribute("alt", alt);
            if (height != null && height > 0)
                imageTagBuilder.MergeAttribute("height", height.ToString());
            if (width != null && width > 0)
                imageTagBuilder.MergeAttribute("width", width.ToString());
            if (!string.IsNullOrEmpty(title))
                imageTagBuilder.MergeAttribute("title", title);
            if (!string.IsNullOrEmpty(cssClass)) 
                imageTagBuilder.MergeAttribute("class", cssClass);
            string imgHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(imgHtml);
        }


        public static MvcHtmlString ImageTag(this HtmlHelper html, MetroTileImage image, string cssClass = null)
        {
            return ImageTag(html, image.PathUri, image.AltText, image.Height, image.Width, image.Title, cssClass);
        }

        public static MvcHtmlString TileImageTag(this HtmlHelper html, MetroTileImage image, string cssClass = null)
        {
            if (string.IsNullOrWhiteSpace(image.PathUri))
                return null;
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", url.Content(image.PathUri));
            imageTagBuilder.MergeAttribute("alt", image.AltText);
            // imageTagBuilder.MergeAttribute("height", "80");
            if (!string.IsNullOrEmpty(image.Title))
                imageTagBuilder.MergeAttribute("title", image.Title);
            if (!string.IsNullOrEmpty(cssClass)) imageTagBuilder.MergeAttribute("class", cssClass);
            string imgHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(imgHtml);
        }

        public static MvcHtmlString SponsorImageTag(this HtmlHelper html, MetroTileImage image, string cssClass = null)
        {
            bool webimage = false;
            var maxWidth = 278;
            var maxHeight = 109;

            if (string.IsNullOrWhiteSpace(image.PathUri))
            {
                webimage = true;
            }

            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imageTagBuilder = new TagBuilder("img");
            if (webimage)
            {
                imageTagBuilder.MergeAttribute("src", "data:image;base64,"+Convert.ToBase64String(image.Logo.GetBytes()));                
            }
            else
            {
                imageTagBuilder.MergeAttribute("src", url.Content(image.PathUri));
            }

            imageTagBuilder.MergeAttribute("alt", image.AltText);

            if (image.Height > maxHeight || image.Width > maxWidth)
            {
                double heightRatio = image.Height > maxHeight ? (double)image.Height / maxHeight : 0.0;
                double widthRatio = image.Width > maxWidth ? (double)image.Width / maxWidth : 0.0;

                if (widthRatio > heightRatio)
                {
                    var multiplier = (int)Math.Floor(d: (double)(image.Width / image.Height));
                    var calculatedWidth = (maxHeight * multiplier);
                    imageTagBuilder.MergeAttribute("width",
                                                   calculatedWidth > maxWidth || calculatedWidth == 0
                                                           ? maxWidth.ToString() : calculatedWidth.ToString());
                }
                else
                {
                    if (image.Height > maxHeight)
                        imageTagBuilder.MergeAttribute("height", maxHeight.ToString());
                    else
                    {
                        imageTagBuilder.MergeAttribute("width", maxWidth.ToString());
                    }
                }
            }

            if (!string.IsNullOrEmpty(image.Title))
                imageTagBuilder.MergeAttribute("title", image.Title);
            if (!string.IsNullOrEmpty(cssClass)) imageTagBuilder.MergeAttribute("class", cssClass);
            var imgHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(imgHtml);
        }
    }
}