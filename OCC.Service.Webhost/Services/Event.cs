namespace OCC.Service.Webhost.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Event
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TwitterHashTag { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Zip { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

        [DataMember]
        public bool IsSponsorRegistrationOpen { get; set; }

        [DataMember]
        public bool IsSpeakerRegistrationOpen { get; set; }

        [DataMember]
        public bool IsAttendeeRegistrationOpen { get; set; }

        [DataMember]
        public bool IsVolunteerRegistrationOpen { get; set; }

        [DataMember]
        public virtual IList<Announcement> Announcements { get; set; }

        [DataMember]
        public virtual IList<Track> Tracks { get; set; }

        [DataMember]
        public virtual IList<Sponsor> Sponsors { get; set; }

        [DataMember]
        public virtual IList<Person> Attendees { get; set; }

        public Event()
        {
            Announcements = new List<Announcement>();
            Tracks = new List<Track>();
            Sponsors = new List<Sponsor>();
            Attendees = new List<Person>();
        }
    }
}