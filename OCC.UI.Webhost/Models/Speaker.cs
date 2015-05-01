namespace OCC.UI.Webhost.Models
{
    using System.Collections.Generic;

    public class Speaker : Person
    {
        public List<Session> Sessions { get; set; }


        public Speaker()
        {
            Sessions = new List<Session>();
        }
    }
}