using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OCC.Data;
using OCC.Service.Webhost.Services;
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

                _dbContext.EventAttendeeSessionRatings.Add(erst);
            }
            _dbContext.SaveChanges();
        }
    }
}