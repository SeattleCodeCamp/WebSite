namespace OCC.UI.Webhost.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public int ID { get; set; }

        [Required, Display(Name = "name")]
        public string Name { get; set; }

        [Required, Display(Name = "description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "twitter hash tag")]
        public string TwitterHashTag { get; set; }

        [Display(Name = "start time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "end time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "location")]
        public string Location { get; set; }

        [Display(Name = "address 1")]
        public string Address1 { get; set; }

        [Display(Name = "address 2")]
        public string Address2 { get; set; }

        [Display(Name = "city")]
        public string City { get; set; }

        [StringLength(2), Display(Name = "state")]
        public string State { get; set; }

        [StringLength(5), Display(Name = "zip")]
        public string Zip { get; set; }

        [Display(Name="is default event")]
        public bool IsDefault { get; set; }

        [Display(Name = "is sponsor registration open")]
        public bool IsSponsorRegistrationOpen { get; set; }

        [Display(Name = "is speaker registration open")]
        public bool IsSpeakerRegistrationOpen { get; set; }

        [Display(Name = "is attendee registration open")]
        public bool IsAttendeeRegistrationOpen { get; set; }

        [Display(Name = "is volunteer registration open")]
        public bool IsVolunteerRegistrationOpen { get; set; }
    }
}