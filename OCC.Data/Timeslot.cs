namespace OCC.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Timeslot
    {
        public int ID { get; set; }

        [ForeignKey("Event")]
        public int Event_ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Event Event { get; set; }

        public List<Session> Sessions { get; set; }

        public Timeslot()
        {
            Sessions = new List<Session>();
        }
    }
}
