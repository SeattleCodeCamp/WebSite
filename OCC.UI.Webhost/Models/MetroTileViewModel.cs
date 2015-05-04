using System.Collections.Generic;
using System.Web.Helpers;

namespace OCC.UI.Webhost.Models
{
    public class MetroTileViewModel
    {
        public MetroTileViewModel()
        {
            InitializeListProperties();
            this.DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/Images/Sponsor.png")
                              {
                                  AltText = ".NET Developers Association Logo",
                                  Title = ".NET Developers Association"
                              };
        }

        public virtual string TileDisplayName { get; set; }
        public virtual MetroTileImage DefaultTileImage { get; set; }
        public virtual IList<MetroTileImage> MetroTileIcons { get; protected set; }
        public virtual string TileLinkActionName { get; set; }
        public virtual string TileLinkControllerName { get; set; }
        public virtual string TileBackgroundCssClass { get; set; }
        public string TileNotificationMessage { get; set; }

        private void InitializeListProperties()
        {
            MetroTileIcons = new List<MetroTileImage>();
        }
    }

    public class DoubleMetroTileViewModel : MetroTileViewModel
    {
        public DoubleMetroTileViewModel()
        {
            this.DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/WhiteIcons/48x48/flag.png")
                              {
                                  AltText = "No Picture",
                                  Title = "No Picture"
                              };
        }

        public override MetroTileImage DefaultTileImage { get; set; }
    }

    public class TwitterMetroTileViewModel
    {
        public TwitterMetroTileViewModel()
        {
            Tweets = new List<TweetViewModel>();
        }

        public IList<TweetViewModel> Tweets { get; set; }
        public string TileDisplayName { get; set; }
        public string TileBackgroundCssClass { get; set; }
    }

    public class TweetViewModel
    {
        public TweetViewModel()
        {
            ProfilePhoto = new MetroTileImage("../../Content/themes/Metro/Images/twitter_newbird_white.png")
                           {
                               AltText = "Follow us on Twitter @SeattleCodeCamp",
                               Title = "Follow us on Twitter @SeattleCodeCamp"
                           };
            TweetContent = "Test tweet for #SeattleCC with consumption of all 146 characters.  This is for the new Seattle Code Camp site that we are currently working.";/*"Follow us on Twitter @SeattleCodeCamp or use the hashtag #SeattleCC";*/
            ProfileName = "Hallmanac";
            ActiveCssClass = "tweetVisible";
        }

        public string TweetContent { get; set; }
        public MetroTileImage ProfilePhoto { get; set; }
        public string ProfileName { get; set; }
        public string ActiveCssClass { get; set; }
    }

    public class MetroTileImage
    {
        // private readonly int maxHeight;
        // private readonly int maxWidth;
        private WebImage image;

        public MetroTileImage(string src)
        {
            try
            {
                this.image = new WebImage(src);
                PathUri = src;
                Height = this.image.Height;
                Width = this.image.Width;
            }
            catch
            {
                // ???
            }
        }

        private string pathUri;

        public string PathUri
        {
            get { return this.pathUri; }
            set
            {
                this.pathUri = value;

                try
                {
                    this.image = new WebImage(this.pathUri);
                    Height = this.image.Height;
                    Width = this.image.Width;
                }
                catch
                { }
            }
        }

        public string AltText { get; set; }
        public string Title { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string AnchorTagUri { get; set; }
    }
}