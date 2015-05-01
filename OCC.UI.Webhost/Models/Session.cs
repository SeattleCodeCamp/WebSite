namespace OCC.UI.Webhost.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Session
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        public int SpeakerID { get; set; }

        public int? TrackID { get; set; }

        public int? TimeslotID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "level")]
        public string Level { get; set; } // ??? was int but was giving a strance error

        public string Status { get; set; }

        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "description")]
        public string Description { get; set; }

        [Display(Name = "speakers")]
        public string Speaker { get; set; }

        public string ImageUrl { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Location { get; set; }

        [Display(Name = "topic")]
        public int? TagID { get; set; }
    }
}