namespace OCC.UI.Webhost.Models
{
    using System.Collections.Generic;

    public class Schedule
    {
        public List<Timeslot> Timeslots { get; set; }

        public List<Track> Tracks { get; set; }

        public List<Session> Sessions { get; set; }

        public Schedule()
        {
            Timeslots = new List<Timeslot>();
            Tracks = new List<Track>();
            Sessions = new List<Session>();
        }
    }
}