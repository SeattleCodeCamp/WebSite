namespace OCC.UI.Webhost.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class Sponsor
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "website url")]
        public string WebsiteUrl { get; set; }

        [Required]
        [Display(Name = "sponsorship level")]
        public string SponsorshipLevel { get; set; }

        [Display(Name = "image url")]
        public string ImageUrl { get; set; }

        //[DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public HttpPostedFileBase Logo { get; set; }
    }
}