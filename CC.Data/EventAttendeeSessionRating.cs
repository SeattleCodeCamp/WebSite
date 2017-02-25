using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CC.Data
{
    public class EventAttendeeSessionRating
    {
        public int Id { get; set; }

        [ForeignKey("EventAttendee")]
        public int EventAttendee_ID { get; set; }

        [ForeignKey("Session")]
        public int Session_ID { get; set; }

        public int Ranking { get; set; }

        [ForeignKey("Timeslot")]
        public int Timeslot_ID { get; set; }

        public string Comments { get; set; }

        public EventAttendee EventAttendee { get; set; }
        public Session Session { get; set; }
        public Timeslot Timeslot { get; set; }
    }
}
