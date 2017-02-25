using System.IO;
using System.Web.Helpers;

namespace CC.UI.Webhost.Infrastructure
{
    public class WebImageOCC : WebImage
    {
            public WebImageOCC(byte[] content)
            : base(content) { }

        public WebImageOCC(string filePath)
            : base(filePath)
        {
            src = filePath;
        }

        public WebImageOCC(Stream imageStream)
            : base(imageStream) { }

        string src;
        public string Src
        {
            get
            {
                return this.src;
            }
            set { this.src = value; }
        }

        public string Title { get; set; }
        public string Alt { get; set; }

    }
}