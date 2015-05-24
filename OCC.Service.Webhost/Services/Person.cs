namespace OCC.Service.Webhost.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Person
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Bio { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public string Blog { get; set; }

        [DataMember]
        public string Twitter { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int TShirtSize { get; set; }
    }
}