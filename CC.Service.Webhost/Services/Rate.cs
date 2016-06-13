using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CC.Service.Webhost.Services
{
    [DataContract]
    public class Rate
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int EventID { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public int RateSignin { get; set; }
        [DataMember]
        public int RateSwag { get; set; }
        [DataMember]
        public int RateFood { get; set; }
        [DataMember]
        public int ReferralSource { get; set; }
        [DataMember]
        public List<RateSession> RatedSessions { get; set; }

        public Rate()
        {
            RatedSessions = new List<RateSession>();
        }
    }

    [DataContract]
    public class RateSession
    {
        [DataMember]
        public int TimeSlotID { get; set; }
        [DataMember]
        public int SessionID { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public string Comments { get; set; }

        public RateSession()
        {

        }
    }

}