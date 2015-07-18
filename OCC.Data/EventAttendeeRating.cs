using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace OCC.Data
{
    public class EventAttendeeRating
    {
        public int ID { get; set; }

        [ForeignKey("EventAttendee")]
        public int EventAttendee_ID { get; set; }

        public int SignIn { get; set; }
        public int Swag { get; set; }
        public int Refreshments { get; set; }
        public int ReferralSource { get; set; }
        public string Comments { get; set; }

        public EventAttendee EventAttendee { get; set; }
    }
}
