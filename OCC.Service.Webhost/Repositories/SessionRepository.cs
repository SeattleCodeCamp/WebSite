using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OCC.Data;
using OCC.Service.Webhost.Tools;
using Session = OCC.Service.Webhost.Services.Session;

namespace OCC.Service.Webhost.Repositories
{
    public class SessionRepository
    {
        private readonly OCCDB _dbContext;

        public SessionRepository(OCCDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Session GetSession(int id)
        {
            var s = (from x in _dbContext.Sessions.Include("Speaker").Include("Track").Include("Timeslot")
                        where x.ID == id
                        select x).FirstOrDefault();

            if (s == null) throw new ArgumentException("Session not found");

            return s.Map();
        }

        public IList<Session> GetSpeakerSessions(int eventId, int speakerId)
        {
            return _dbContext.Sessions.Where(s => s.Speaker_ID == speakerId && s.Event_ID == eventId)
                .Select(s => new Session()
                {
                    ID = s.ID,
                    EventID = s.Event_ID,
                    SpeakerID = s.Speaker_ID,
                    Name = s.Name,
                    Description = s.Description,
                    Status = s.Status,
                    Level = s.Level,
                    Location = s.Location
                }).ToList();
        }
    }
}