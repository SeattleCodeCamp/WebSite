namespace OCC.Service.Webhost.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Timeslot
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int EventID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }
    }
}