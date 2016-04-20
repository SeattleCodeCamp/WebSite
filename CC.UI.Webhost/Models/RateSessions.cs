using System.ComponentModel.DataAnnotations;

namespace CC.UI.Webhost.Models
{
    public class RateSessions
    {
        public int ID { get; set; }

        public int SessionRating { get; set; }

        public int SpeakerRating { get; set; }

        [StringLength(2000)]
        public string Comment { get; set; }

        public RateSessions()
        {

        }
    }
}