namespace OCC.Service.Webhost.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Sponsor
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
        public string SponsorshipLevel { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string WebsiteUrl { get; set; }

        [DataMember]
        public byte[] Image { get; set; }
    }
}