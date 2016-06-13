using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CC.Service.Webhost.Services
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string TagName { get; set; }

        [DataMember]
        public int SessionsCount { get; set; }

        public Tag()
        {

        }
    }
}