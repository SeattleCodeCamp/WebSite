namespace OCC.UI.Webhost.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VolunteerTask
    {
        public int Id { get; set; }

        [Display(Name = "event")]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "description")]
        public string Description { get; set; }

        public IList<Volunteer> Volunteers { get; set; }

        [Display(Name = "capacity")]
        public int Capacity { get; set; }

        [Display(Name = "start time")]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "end time")]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime EndTime { get; set; }

        public Event Event { get; set; }

        public VolunteerTask()
        {
            Volunteers = new List<Volunteer>();
        }
    }
}