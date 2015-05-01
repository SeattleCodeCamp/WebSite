namespace OCC.UI.Webhost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using OCC.UI.Webhost.Models;

    public class ScheduleController : BaseController
    {
        //
        // GET: /Schedule/

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int eventid)
        {
            Schedule model = new Schedule();

            var timeslots = service.GetTimeslots(eventid);
            foreach (var timeslot in timeslots)
                model.Timeslots.Add(new Timeslot()
                {
                    ID = timeslot.ID,
                    EventID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                });

            var tracks = service.GetTracks(eventid);
            foreach (var track in tracks)
                model.Tracks.Add(new Track()
                {
                    ID = track.ID,
                    EventID = track.EventID,
                    Name = track.Name,
                    Description = track.Description
                });

            var sessions = service.GetSessions(eventid);
            foreach (var session in sessions)
                model.Sessions.Add(new Session()
                {
                    ID = session.ID,
                    TrackID = session.TrackID,
                    TimeslotID = session.TimeslotID,
                    Name = session.Name,
                    Description = session.Description,
                    Level = session.Level.ToString(),
                    Status = session.Status,
                    Speaker = session.Speaker,
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                });

            return View(model);
        }

        [HttpPost]
        public void Schedule(string target, string session)
        {
            string[] t = target.Substring(2).Split('_');
            int timeslotId = Int32.Parse(t[0]);
            int trackId = Int32.Parse(t[1]);
            int sessionId = Int32.Parse(session.Substring(2));

            service.Schedule(sessionId, trackId, timeslotId);
        }
    }
}
