namespace CC.UI.Webhost.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

    public class Announcement
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "title")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "content")]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [Display(Name = "publish date")]
        public DateTime PublishDate { get; set; }
    }
}