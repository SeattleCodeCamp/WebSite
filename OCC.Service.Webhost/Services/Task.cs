using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OCC.Service.Webhost.Services
{
    [DataContract]
    public class Task
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EventID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public virtual IList<Person> Assignees { get; set; }

        [DataMember]
        public int Capacity { get; set; }

        [DataMember]
        public Event Event { get; set; }

        public Task()
        {
            Assignees = new List<Person>();
        }
    }
}