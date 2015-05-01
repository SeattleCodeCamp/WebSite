namespace OCC.UI.Webhost.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Track
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "description")]
        public string Description { get; set; }

        public IList<Session> Sessions { get; set; }

        public Track()
        {
            Sessions = new List<Session>();
        }
    }
}