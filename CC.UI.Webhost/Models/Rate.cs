using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.UI.Webhost.Models
{
    public class Rate
    {
        public int EventAttendeeID { get; set; }
        public string Comments { get; set; }
        public int RateSignin { get; set; }
        public int RateSwag { get; set; }
        public int RateFood { get; set; }
        public List<RateSession> RateSessions { get; set; } 

        public Rate()
        {
            RateSessions = new List<RateSession>();
        }
    }

    public class RateSession
    {
        public int TimeSlotID { get; set; }
        public int SessionID { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

        public RateSession()
        {

        }
    }
}