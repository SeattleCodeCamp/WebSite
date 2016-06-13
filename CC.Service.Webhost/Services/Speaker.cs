namespace CC.Service.Webhost.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    //[DataContract]
    //public class Speaker
    //{
    //    [DataMember]
    //    public int ID { get; set; }

    //    [DataMember]
    //    public string Email { get; set; }

    //    [DataMember]
    //    public string Name { get; set; }

    //    [DataMember]
    //    public string Title { get; set; }

    //    [DataMember]
    //    public string Bio { get; set; }

    //    [DataMember]
    //    public string Website { get; set; }

    //    [DataMember]
    //    public string Blog { get; set; }

    //    [DataMember]
    //    public string Twitter { get; set; }

    //    [DataMember]
    //    public string ImageUrl { get; set; }

    //    [DataMember]
    //    public IList<Session> Sessions { get; set; }

    //    public Speaker()
    //    {
    //        Sessions = new List<Session>();
    //    }
    //}

    [DataContract]
    public class Speaker : Person
    {
        [DataMember]
        public IList<Session> Sessions { get; set; }

        public Speaker()
        {
            Sessions = new List<Session>();
        }
    }
}