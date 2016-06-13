using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CC.Service.Webhost.Services
{
    [DataContract]
    public class SessionAttendee
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string SessionName { get; set; }

        [DataMember]
        public string SpeakerName { get; set; }

        [DataMember]
        public int? SessionRating { get; set; }

        [DataMember]
        public int? SpeakerRating { get; set; }

        [DataMember]
        public string Comment { get; set; }
    }
}