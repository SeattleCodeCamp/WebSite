using System;
using System.Collections.Generic;
using System.Linq;
using CC.Data;
using CC.Service.Webhost.Services;
using CC.Service.Webhost.Tools;
using Session = CC.Service.Webhost.Services.Session;

namespace CC.Service.Webhost.Repositories
{
    public class SessionRepository : RepositoryBase
    {
        public SessionRepository() : base(new CCDB())
        {
        }
        public SessionRepository(CCDB dbContext)
            : base(dbContext)
        {
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

        public void CreateRateSession(Rate rating)
        {
            EventAttendee et = _dbContext.EventAttendees.Where(e => e.Event_ID == rating.EventID && e.Person_ID == rating.UserID).FirstOrDefault();
            EventAttendeeRating ert = new EventAttendeeRating();
            ert.Comments = rating.Comments;
            ert.EventAttendee_ID = et.ID;
            ert.ReferralSource = rating.ReferralSource;
            ert.Refreshments = rating.RateFood;
            ert.SignIn = rating.RateSignin;
            ert.Swag = rating.RateSwag;
            _dbContext.EventAttendeeRatings.Add(ert);
            _dbContext.SaveChanges();
            foreach (RateSession rateSession in rating.RatedSessions)
            {
                EventAttendeeSessionRating erst = new EventAttendeeSessionRating();
                erst.EventAttendee_ID = et.ID;
                erst.Ranking = rateSession.Rating;
                erst.Session_ID = rateSession.SessionID;
                erst.Timeslot_ID = rateSession.TimeSlotID;
                erst.Comments = rateSession.Comments;

                _dbContext.EventAttendeeSessionRatings.Add(erst);
            }
            _dbContext.SaveChanges();
        }

        public void CreateSession(Session session)
        {
            Data.Session s = new Data.Session()
            {
                Event_ID = session.EventID,
                Speaker_ID = session.SpeakerID,
                Name = session.Name,
                Description = session.Description,
                Level = session.Level,
                Location = session.Location,
                Status = session.Status,
                Tag_ID = session.TagID.Value
            };

            _dbContext.Sessions.Add(s);
            _dbContext.SaveChanges();
        }

        public void UpdateSession(Session session)
        {
            var s = _dbContext.Sessions.Find(session.ID);

            s.Name = session.Name;
            s.Description = session.Description;
            s.Level = session.Level;
            s.Location = session.Location;
            s.Status = session.Status;
            s.Tag_ID = session.TagID;
            _dbContext.SaveChanges();
        }

        public void DeleteSession(int id)
        {
            Data.Session session = (from s in _dbContext.Sessions.Include("Attendees") where s.ID == id select s).FirstOrDefault();

            if (session.Attendees.Count > 0)
                throw new Exception("Can't delete a session that contains attendees!");

            _dbContext.Sessions.Remove(session);

            _dbContext.SaveChanges();
        }
    }
}