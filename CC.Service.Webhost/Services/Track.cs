namespace CC.Service.Webhost.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Track
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int EventID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public virtual IList<Session> Sessions { get; set; }

        public Track()
        {
            Sessions = new List<Session>();
        }
    }
}