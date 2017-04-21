namespace CC.Service.Webhost.Services
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Session
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int EventID { get; set; }

        [DataMember]
        public int SpeakerID { get; set; }

        [DataMember]
        public int? TrackID { get; set; }

        [DataMember]
        public int? TimeslotID { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string Status { get; set; }
        
        [DataMember]
        public string Track { get; set; }

        [DataMember]
        public string Speaker { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public DateTime? StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int? TagID { get; set; }
    }
}