using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    public class JsonTypes
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string bio { get; set; }
        public string tagLine { get; set; }
        public string profilePicture { get; set; }
        public Session[] sessions { get; set; }
        public bool isTopSpeaker { get; set; }
        public Link[] links { get; set; }
    }

    public class Session
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Link
    {
        public string title { get; set; }
        public string url { get; set; }
        public string linkType { get; set; }
    }


    public class AllSessions
    {
        public object groupId { get; set; }
        public string groupName { get; set; }
        public Session2[] sessions { get; set; }
    }

    public class Session2
    {
        public object[] questionAnswers { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startsAt { get; set; }
        public DateTime endsAt { get; set; }
        public bool isServiceSession { get; set; }
        public bool isPlenumSession { get; set; }
        public Speaker2[] speakers { get; set; }
        public Category[] categories { get; set; }
        public int roomId { get; set; }
        public string room { get; set; }
    }

    public class Speaker2
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Categoryitem[] categoryItems { get; set; }
        public int sort { get; set; }
    }

    public class Categoryitem
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
