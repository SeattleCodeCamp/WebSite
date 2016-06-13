using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.UI.Webhost.Models
{
    public class SessionsViewModel
    {
        public SessionsViewModel()
        {
            Sessions = new List<Session>();
            Timeslots = new List<Timeslot>();
        }
        public List<Session> Sessions { get; set; }
        public List<Timeslot> Timeslots { get; set; } 
    }
}