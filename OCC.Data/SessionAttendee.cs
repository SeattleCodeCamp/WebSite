using System.ComponentModel.DataAnnotations.Schema;

namespace OCC.Data
{
    using System.ComponentModel.DataAnnotations;

    public class SessionAttendee
    {
        public int ID { get; set; }

        [ForeignKey("Session")]
        public int Session_ID { get; set; }

        [ForeignKey("Person")]
        public int Person_ID { get; set; }

        public Session Session { get; set; }

        public Person Person { get; set; }

        public int SessionRating { get; set; }

        public int SpeakerRating { get; set; }

        [StringLength(2000)]
        public string Comment { get; set; }
    }
}