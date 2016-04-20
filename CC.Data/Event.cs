namespace CC.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string TwitterHashTag { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(5)]
        public string Zip { get; set; }

        public bool IsDefault { get; set; }

        public bool IsSponsorRegistrationOpen { get; set; }

        public bool IsSpeakerRegistrationOpen { get; set; }

        public bool IsAttendeeRegistrationOpen { get; set; }

        public bool IsVolunteerRegistrationOpen { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public virtual ICollection<Sponsor> Sponsors { get; set; }

        public virtual ICollection<EventAttendee> Attendees { get; set; }

        public Event()
        {
            Announcements = new List<Announcement>();

            Tracks = new List<Track>();
            
            Timeslots = new List<Timeslot>();
            
            Sponsors = new List<Sponsor>();
            
            Attendees = new List<EventAttendee>();
        }
    }
}