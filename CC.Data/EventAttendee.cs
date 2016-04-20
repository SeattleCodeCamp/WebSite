using System.ComponentModel.DataAnnotations.Schema;

namespace CC.Data
{
    using System.ComponentModel.DataAnnotations;

    public class EventAttendee
    {
        public int ID { get; set; }

        [ForeignKey("Event")]
        public int Event_ID { get; set; }

        [ForeignKey("Person")]
        public int Person_ID { get; set; }

        [StringLength(10)]
        public string Rsvp { get; set; }

        public Event Event { get; set; }

        public Person Person { get; set; }
    }
}