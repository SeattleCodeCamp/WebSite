namespace OCC.UI.Webhost.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Timeslot
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "start time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "end time")]
        public DateTime EndTime { get; set; }

        public Timeslot()
        {
        }
    }
}