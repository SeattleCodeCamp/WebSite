﻿using Microsoft.Azure.KeyVault;
using System.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using CC.Service.Webhost.Services;
using CC.Service.Webhost.Repositories;
using CC.Service.Webhost.Tools;
using Ninject;
using OCC.Service.Webhost.Tools;
using System.Configuration;
using Tasks = System.Threading.Tasks;

namespace CC.Service.Webhost.CodeCampSvc
{
    public class CodeCampService : ICodeCampService
    {
        private readonly PersonRepository _personRepository = new PersonRepository();
        private readonly SessionRepository _sessionRepository = new SessionRepository();
        private readonly MetadataRepository _metadataRepository = new MetadataRepository();
        private readonly TaskRepository _taskRepository = new TaskRepository();
        private readonly TagRepository _tagRepository = new TagRepository();
        private const string ApprovedSession = "APPROVED";
        private const string SubmittedSession = "SUBMITTED";

        //public CodeCampService(StandardKernel kernel)
        //{
        //    _personRepository = kernel.Get<PersonRepository>();
        //    _sessionRepository = kernel.Get<SessionRepository>();
        //    _metadataRepository = kernel.Get<MetadataRepository>();
        //    _taskRepository = kernel.Get<TaskRepository>();
        //    _tagRepository = kernel.Get<TagRepository>();
        //}

        public CodeCampService(PersonRepository personRepo, SessionRepository sessionRepo, MetadataRepository metaRepo, TaskRepository taskRepo, TagRepository tagRepo)
        {
            _personRepository = personRepo;
            _sessionRepository = sessionRepo;
            _metadataRepository = metaRepo;
            _taskRepository = taskRepo;
            _tagRepository = tagRepo;
        }

        public CodeCampService()
        {
            _personRepository = new PersonRepository();
            _sessionRepository = new SessionRepository();
            _metadataRepository = new MetadataRepository();
            _taskRepository = new TaskRepository();
            _tagRepository = new TagRepository();
        }

        #region People

        public int RegisterPerson(Person person)
        {
            return _personRepository.RegisterPerson(person);
        }

        public Person Login(Person person)
        {
            return _personRepository.Login(person);
        }

        public Person FindPersonByEmail(string email, string provider)
        {
            return _personRepository.FindPersonByEmail(email, provider);
        }

        public Person FindPersonByEmail(string email)
        {
            return _personRepository.FindPersonByEmail(email);
        }

        public void ResetPassword(string emailAddress, string temporaryPassword, string temporaryPasswordHash)
        {
            _personRepository.ResetPassword(emailAddress, temporaryPassword, temporaryPasswordHash);
        }

        public void ChangePassword(int id, string oldPasswordHash, string newPasswordHash)
        {
            _personRepository.ChangePassword(id, oldPasswordHash, newPasswordHash);
        }

        public void UpdatePerson(Person person)
        {
            _personRepository.UpdatePerson(person);
        }

        public void DeletePerson(int personId)
        {
            throw new NotImplementedException();
        }

        public bool HasSubmittedRating(int personid, int eventid)
        {
            bool flag = false;
            using (var db = new CC.Data.CCDB())
            {
                CC.Data.EventAttendee et = db.EventAttendees.Where(e => e.Event_ID == eventid && e.Person_ID == personid).FirstOrDefault();
                if (et == null)
                    return false;
                flag = db.EventAttendeeRatings.Where(e => e.EventAttendee_ID == et.ID).Any();
            }
            return flag;
        }

        public IList<Person> GetAdministrators()
        {
            List<Person> result = new List<Person>();

            using (var db = new CC.Data.CCDB())
            {
                var admins = (from p in db.People
                              where p.IsAdmin
                              select p)
                        .OrderBy(s => s.FirstName + " " + s.LastName)
                        .ToList();

                foreach (var admin in admins)
                    result.Add(admin.Map());
            }

            return result;
        }

        public void AddAdministrator(int personId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var p = db.People.Find(personId);

                if (p == null) throw new ArgumentException("Person not found");

                p.IsAdmin = true;

                db.SaveChanges();
            }
        }

        public void RemoveAdministrator(int personId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var p = db.People.Find(personId);

                if (p == null) throw new ArgumentException("Person not found");

                p.IsAdmin = false;

                db.SaveChanges();
            }
        }

        #endregion

        #region Events

        public IList<Event> GetEvents()
        {
            using (var db = new CC.Data.CCDB())
            {
                List<Event> result = new List<Event>();

                foreach (var e in db.Events) result.Add(e.Map());

                return result;
            }
        }

        // Refactored into TagRepository
        //public IList<Tag> GetTags()
        //{
        //    using (var db = new CC.Data.OCCDB())
        //    {
        //        List<Tag> result = new List<Tag>();
        //        //var sessions = db.Sessions;
        //        //var tags = db.Tags.OrderBy(t => t.TagName);
        //        var sessionsTags = from t in db.Tags
        //                           select new { t.ID, t.TagName, SessionsCount = db.Sessions.Where(s => s.Tag_ID == t.ID).Count() };
        //        foreach (var tag in sessionsTags)
        //        {
        //            Data.Tag tg = new Data.Tag() { ID = tag.ID, TagName = tag.TagName };
        //            int count = tag.SessionsCount;
        //            result.Add(tg.Map(count));
        //        }

        //        return result;
        //    }

        //}

        public IList<Tag> GetTagsByEvent(int eventid)
        {
            using (var db = new CC.Data.CCDB())
            {
                List<Tag> result = new List<Tag>();
                //var sessions = db.Sessions;
                //var tags = db.Tags.OrderBy(t => t.TagName);
                var sessionsTags = from t in db.Tags
                                   select new { t.ID, t.TagName, SessionsCount = db.Sessions.Where(s => s.Tag_ID == t.ID && s.Event_ID == eventid).Count() };
                foreach (var tag in sessionsTags)
                {
                    Data.Tag tg = new Data.Tag() { ID = tag.ID, TagName = tag.TagName };
                    int count = tag.SessionsCount;
                    result.Add(tg.Map(count));
                }

                return result;
            }

        }

        public IList<Event> GetEventsByDate(DateTime fromDate, DateTime toDate)
        {
            using (var db = new CC.Data.CCDB())
            {
                List<Event> result = new List<Event>();

                var events = (from e in db.Events where fromDate <= e.StartTime && e.StartTime <= toDate select e).ToList();

                foreach (var e in events) result.Add(e.Map());

                return result;
            }
        }

        public void CreateEvent(Event event_)
        {
            using (var db = new CC.Data.CCDB())
            {
                if (event_.IsDefault)
                {
                    var defaultEvent = db.Events.Where(x => x.IsDefault).FirstOrDefault();
                    if (defaultEvent != null)
                        defaultEvent.IsDefault = false;
                }

                Data.Event e = new Data.Event();

                e.Name = event_.Name;
                e.Description = event_.Description;
                e.TwitterHashTag = event_.TwitterHashTag;
                e.StartTime = event_.StartTime;
                e.EndTime = event_.EndTime;
                e.Location = event_.Location;
                e.Address1 = event_.Address1;
                e.Address2 = event_.Address2;
                e.City = event_.City;
                e.State = event_.State;
                e.Zip = event_.Zip;
                e.IsDefault = event_.IsDefault;
                e.IsSponsorRegistrationOpen = event_.IsSponsorRegistrationOpen;
                e.IsSpeakerRegistrationOpen = event_.IsSpeakerRegistrationOpen;
                e.IsAttendeeRegistrationOpen = event_.IsAttendeeRegistrationOpen;
                e.IsVolunteerRegistrationOpen = event_.IsVolunteerRegistrationOpen;

                db.Events.Add(e);
                db.SaveChanges();
            }
        }

        public async Tasks.Task Init()
        {
            if (string.IsNullOrWhiteSpace(AKV.EncryptSecret))
            {
                var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(AKV.GetToken));
                var sec = await kv.GetSecretAsync(ConfigurationManager.AppSettings["SecretUri"]);

                AKV.EncryptSecret = sec.Value;
            }
        }

        public Event GetEvent(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Find(eventId);

                if (e == null)
                    throw new ArgumentException("Event not found");
                var result = e.Map();
                return result;
            }
        }

        public void UpdateEvent(Event event_)
        {
            using (var db = new CC.Data.CCDB())
            {
                var defaultEvent = db.Events.Where(x => x.IsDefault).FirstOrDefault();
                if (event_.IsDefault)
                {
                    if (defaultEvent != null) defaultEvent.IsDefault = false;
                }
                else
                {
                    if (defaultEvent == null) event_.IsDefault = true;
                }

                var e = db.Events.Find(event_.ID);

                e.Name = event_.Name;
                e.Description = event_.Description;
                e.TwitterHashTag = event_.TwitterHashTag;
                e.StartTime = event_.StartTime;
                e.EndTime = event_.EndTime;
                e.Location = event_.Location;
                e.Address1 = event_.Address1;
                e.Address2 = event_.Address2;
                e.City = event_.City;
                e.State = event_.State;
                e.Zip = event_.Zip;
                e.IsDefault = event_.IsDefault;
                e.IsSponsorRegistrationOpen = event_.IsSponsorRegistrationOpen;
                e.IsSpeakerRegistrationOpen = event_.IsSpeakerRegistrationOpen;
                e.IsAttendeeRegistrationOpen = event_.IsAttendeeRegistrationOpen;
                e.IsVolunteerRegistrationOpen = event_.IsVolunteerRegistrationOpen;

                db.SaveChanges();
            }
        }

        public Event GetDefaultEvent()
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Where(x => x.IsDefault).FirstOrDefault();

                if (e == null)
                    throw new ArgumentException("Event not found");

                return e.Map();
            }
        }

        #endregion

        #region Sponsors

        public IList<Sponsor> GetSponsors(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Find(eventId);

                if (e == null)
                    throw new ArgumentException("Event not found");

                var result = new List<Sponsor>();
                // results.Add(new Sponsor { ID = 1, Name = "Microsoft" });
                // results.Add(new Sponsor { ID = 2, Name = "DevExpress" });

                foreach (var s in e.Sponsors.OrderBy(sp => Guid.NewGuid()))
                {
                    Sponsor sponsor = new Sponsor();

                    Mapper.CopyProperties(s, sponsor);

                    result.Add(sponsor);
                }

                return result;
            }
        }

        public Sponsor GetSponsor(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                var sponsor = db.Sponsors.Find(id);

                Sponsor result = new Sponsor();

                Mapper.CopyProperties(sponsor, result);

                return result;
            }
        }

        public void CreateSponsor(Sponsor sponsor)
        {
            using (var db = new CC.Data.CCDB())
            {
                db.Sponsors.Add(sponsor.Map());

                db.SaveChanges();
            }
        }

        public void UpdateSponsor(Sponsor sponsor)
        {
            using (var db = new CC.Data.CCDB())
            {
                var s = db.Sponsors.Find(sponsor.ID);

                s.Name = sponsor.Name;
                s.Description = sponsor.Description;
                s.WebsiteUrl = sponsor.WebsiteUrl;
                s.SponsorshipLevel = sponsor.SponsorshipLevel;
                //s.ImageUrl = sponsor.ImageUrl;
                s.Image = sponsor.Image;

                db.SaveChanges();
            }
        }

        public void DeleteSponsor(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                var s = db.Sponsors.Find(id);

                db.Sponsors.Remove(s);

                db.SaveChanges();
            }
        }

        #endregion

        #region Announcements

        public IList<Announcement> GetCurrentAnnouncements(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var announcements =
                    (from a in db.Announcements
                     where a.Event_ID == eventId && a.PublishDate <= DateTime.Today
                     orderby a.PublishDate descending
                     select a).ToList();

                var result = new List<Announcement>();

                foreach (var announcement in announcements)
                    result.Add(announcement.Map());

                return result;
            }
        }

        public IList<Announcement> GetAnnouncements(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var announcements =
                    (from a in db.Announcements
                     where a.Event_ID == eventId
                     orderby a.PublishDate descending
                     select a).ToList();

                var result = new List<Announcement>();

                foreach (var announcement in announcements)
                    result.Add(announcement.Map());

                //foreach (var announcement in db.Events.Find(eventId).Announcements)
                //{
                //    // var a = new Announcement();
                //    // Mapper.CopyProperties(announcement, a);
                //    result.Add(announcement.Map());
                //}

                return result;
            }
        }

        public Announcement GetAnnouncement(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                var announcement = db.Announcements.Find(id);

                if (announcement == null) throw new ArgumentException("Announcement not found");

                // Announcement result = new Announcement();

                // Mapper.CopyProperties(announcement, result);

                return announcement.Map(); // result;
            }
        }

        public void CreateAnnouncement(Announcement announcement)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Announcement a = new Data.Announcement()
                {
                    Event_ID = announcement.EventID,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    PublishDate = announcement.PublishDate
                };

                db.Announcements.Add(a);

                db.SaveChanges();
            }
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            using (var db = new CC.Data.CCDB())
            {
                var a = db.Announcements.Find(announcement.ID);

                if (a == null) throw new ArgumentException("Announcement not found");

                a.Title = announcement.Title;
                a.Content = announcement.Content;
                a.PublishDate = announcement.PublishDate;

                db.SaveChanges();
            }
        }

        public void DeleteAnnouncement(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Announcement announcement = (from a in db.Announcements where a.ID == id select a).FirstOrDefault();

                db.Announcements.Remove(announcement);

                db.SaveChanges();
            }
        }

        #endregion

        #region Tracks

        public IList<Track> GetTracks(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Find(eventId);

                if (e == null)
                    throw new ArgumentException("Event not found");

                List<Track> result = new List<Track>();
                foreach (var track in e.Tracks)
                    result.Add(track.Map());

                return result;
            }
        }

        public Track GetTrack(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                var t = db.Tracks.Find(id);

                if (t == null) throw new ArgumentException("Track not found");

                return t.Map();
            }
        }

        public void CreateTrack(Track track)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Track t = new Data.Track()
                {
                    Event_ID = track.EventID,
                    Name = track.Name,
                    Description = track.Description
                };

                db.Tracks.Add(t);

                db.SaveChanges();
            }
        }

        public void UpdateTrack(Track track)
        {
            using (var db = new CC.Data.CCDB())
            {
                var t = db.Tracks.Find(track.ID);

                t.Name = track.Name;
                t.Description = track.Description;

                db.SaveChanges();
            }
        }

        public void DeleteTrack(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Track track = (from t in db.Tracks.Include("Sessions") where t.ID == id select t).FirstOrDefault();

                if (track.Sessions.Count > 0)
                    throw new Exception("Can't delete a track that contains sessions!");

                db.Tracks.Remove(track);

                db.SaveChanges();
            }
        }

        #endregion

        #region Timeslots

        public IList<Timeslot> GetTimeslots(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Find(eventId);

                if (e == null)
                    throw new ArgumentException("Event not found");

                List<Timeslot> result = new List<Timeslot>();
                foreach (var Timeslot in e.Timeslots)
                    result.Add(Timeslot.Map());

                return result;
            }
        }

        public Timeslot GetTimeslot(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                var t = db.Timeslots.Find(id);

                if (t == null) throw new ArgumentException("Timeslot not found");

                return t.Map();
            }
        }

        public void CreateTimeslot(Timeslot timeslot)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Timeslot t = new Data.Timeslot()
                {
                    Event_ID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                };

                db.Timeslots.Add(t);

                db.SaveChanges();
            }
        }

        public void UpdateTimeslot(Timeslot timeslot)
        {
            //ToDo: Add error handling here. Brian Hall had an unhandled exception when he tried to submit a timeslot that was a past date-time.
            using (var db = new CC.Data.CCDB())
            {
                var t = db.Timeslots.Find(timeslot.ID);

                t.Name = timeslot.Name;
                t.StartTime = timeslot.StartTime;
                t.EndTime = timeslot.EndTime;

                db.SaveChanges();
            }
        }

        public void DeleteTimeslot(int id)
        {
            using (var db = new CC.Data.CCDB())
            {
                Data.Timeslot Timeslot = (from t in db.Timeslots.Include("Sessions")
                                          where t.ID == id
                                          select t).FirstOrDefault();

                if (Timeslot.Sessions.Count > 0)
                    throw new Exception("Can't delete a Timeslot that contains sessions!");

                db.Timeslots.Remove(Timeslot);

                db.SaveChanges();
            }
        }

        #endregion

        #region Sessions

        public IList<Session> GetSessions(int eventId)
        {
            var eventInfo = GetEvent(eventId);

            if (!eventInfo.IsSpeakerRegistrationOpen)
            {
                return GetApprovedSessions(eventId);
            }
            else
            {
                using (var db = new CC.Data.CCDB())
                {
                    var sessions = (from s in db.Sessions.Include("Speaker")
                                    where s.Event_ID == eventId
                                    orderby s.Name
                                    select s).ToList();

                    var result = new List<Session>();

                    foreach (var session in sessions)
                        result.Add(session.Map());

                    return result;
                }
            }
        }

        public IList<Session> GetApprovedSessions(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var sessions = (from s in db.Sessions.Include("Speaker")
                                where s.Event_ID == eventId
                                && s.Status == ApprovedSession
                                orderby s.Name
                                select s).ToList();

                var result = new List<Session>();

                foreach (var session in sessions)
                    result.Add(session.Map());

                return result;
            }
        }

        public Session GetSession(int id)
        {
            // TODO: determine if there are any approved sessions and show only them, otherwise show all
            return _sessionRepository.GetSession(id);
        }

        public IList<Session> GetSpeakerSessions(int eventId, int speakerId)
        {
            return _sessionRepository.GetSpeakerSessions(eventId, speakerId);
        }

        public void CreateRateSession(Rate rating)
        {
            _sessionRepository.CreateRateSession(rating);
        }

        public void CreateSession(Session session)
        {
            _sessionRepository.CreateSession(session);
        }

        public void UpdateSession(Session session)
        {
            _sessionRepository.UpdateSession(session);
        }

        public void DeleteSession(int id)
        {
            _sessionRepository.DeleteSession(id);
        }

        #endregion

        #region Speakers

        public IList<Person> GetAllAttendees(int eventId)
        {
            var eventInfo = GetEvent(eventId);
            List<Person> result = new List<Person>();
            using (var db = new CC.Data.CCDB())
            {
                var people = db.EventAttendees.Where(e => e.Event_ID == eventId && e.Rsvp == "YES").Select(e => e.Person).ToList();
                foreach (var person in people)
                {
                    result.Add(person.Map());
                }
            }
            return result;
        }

        public IList<Speaker> GetSpeakers(int eventId)
        {
            var eventInfo = GetEvent(eventId);

            List<Speaker> result = new List<Speaker>();

            using (var db = new CC.Data.CCDB())
            {

                var sessions = (from session in db.Sessions.Include("Speakers").Include("Sessions")
                                where session.Event_ID == eventId
                                select session);

                //if registrations are closed and there are approved sessions, filter this list
                if (!eventInfo.IsSpeakerRegistrationOpen && (sessions.Where(s => s.Status == ApprovedSession).Any()))
                {
                    sessions = sessions.Where(s => s.Status == ApprovedSession);
                }

                var speakers = sessions.Select(s => s.Speaker)
                        .Distinct()
                        .OrderBy(s => s.LastName + " " + s.FirstName)
                        .ToList();

                foreach (var speaker in speakers)
                    result.Add(speaker.AsSpeaker());
            }

            return result;
        }

        public Speaker GetSpeaker(int eventId, int speakerId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var s = (from speaker in db.People.Include("Sessions")
                         where speaker.ID == speakerId
                         select speaker).FirstOrDefault();

                if (s == null) throw new ArgumentException("Speaker not found");

                // OLD: return s.AsSpeaker(); // ?? how to filter sessions by event id ??

                Speaker result = new Speaker()
                {
                    ID = s.ID,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Title = string.IsNullOrEmpty(s.Title) ? string.Empty : s.Title,
                    Bio = string.IsNullOrEmpty(s.Bio) ? string.Empty : s.Bio,
                    Website = string.IsNullOrEmpty(s.Website) ? string.Empty : s.Website,
                    Blog = string.IsNullOrEmpty(s.Blog) ? string.Empty : s.Blog,
                    Twitter = string.IsNullOrEmpty(s.Twitter) ? string.Empty : s.Twitter,
                    ImageUrl = s.ImageUrl
                };

                foreach (var session in s.Sessions)
                    if (session.Event_ID == eventId)
                    {
                        var tSlot = (from timeslot in db.Timeslots
                                     where timeslot.ID == session.Timeslot_ID
                                     select timeslot);
                        var tRack = (from track in db.Tracks
                                     where track.ID == session.Track_ID
                                     select track);
                        if (tSlot != null)
                        {
                            session.Timeslot = tSlot.FirstOrDefault();
                        }
                        if (tRack != null)
                        {
                            session.Track = tRack.FirstOrDefault();
                        }

                        result.Sessions.Add(session.Map());
                    }

                return result;
            }
        }

        #endregion

        #region Volunteers

        public IList<Person> GetTaskAssignees(int taskId)
        {
            return _taskRepository.GetTaskAssignees(taskId);
        }

        public IList<Task> GetAllCurrentEventTasks(int eventId)
        {
            return _taskRepository.GetAllCurrentEventTasks(eventId);
        }

        public IList<Task> GetTasksForAssignee(int eventId, int personId)
        {
            return _taskRepository.GetTasksForAssignee(eventId, personId);
        }

        public void AssignVolunteerTaskToPerson(Task task, Person person)
        {
            _taskRepository.AssignVolunteerTaskToPerson(task, person);
        }

        public void AssignTaskToPerson(Task task)
        {
            _taskRepository.AssignTaskToPerson(task);
        }

        public void RemoveTaskFromPerson(Task task)
        {
            _taskRepository.RemoveTaskFromPerson(task);
        }

        public Task GetTaskById(int taskId)
        {
            return _taskRepository.GetTaskById(taskId);
        }

        public void AddTaskToEvent(Task newTask)
        {
            _taskRepository.AddTaskToEvent(newTask);
        }

        public void DisableTask(int existingTaskId)
        {
            _taskRepository.DisableTask(existingTaskId);
        }

        public void UpdateTask(Task existingTask)
        {
            _taskRepository.UpdateTask(existingTask);
        }

        #endregion

        #region EventAttendee

        public string GetEventAttendee(int eventId, int personId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var ea = db.EventAttendees
                    .Where(x => x.Event_ID == eventId && x.Person_ID == personId)
                    .FirstOrDefault();

                if (ea == null)
                    return "";
                else
                    return ea.Rsvp;
            }
        }

        public void Rsvp(int eventId, int personId, string rsvp)
        {
            using (var db = new CC.Data.CCDB())
            {
                var ea = db.EventAttendees
                    .Where(x => x.Event_ID == eventId && x.Person_ID == personId)
                    .FirstOrDefault();

                if (ea == null)
                {
                    ea = new CC.Data.EventAttendee() { Event_ID = eventId, Person_ID = personId, Rsvp = rsvp };
                    db.EventAttendees.Add(ea);
                }
                else
                {
                    ea.Rsvp = rsvp;
                }

                db.SaveChanges();
            }
        }

        #endregion

        #region Schedule

        public void Schedule(int sessionId, int trackId, int timeslotId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var session = db.Sessions.Find(sessionId);

                if (session == null) throw new Exception("Session not found");

                session.Track_ID = trackId == 0 ? (int?)null : trackId;
                session.Timeslot_ID = timeslotId == 0 ? (int?)null : timeslotId;
                session.Status = trackId == 0 ? SubmittedSession : ApprovedSession;


                db.SaveChanges();
            }
        }

        #endregion

        #region Agenda

        public IList<Track> GetAgenda(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = (from x in db.Events.Include("Tracks.Sessions.Speaker").Include("Tracks.Sessions.Timeslot")
                         where x.ID == eventId
                         select x).FirstOrDefault();

                if (e == null)
                    throw new ArgumentException("Event not found");

                List<Track> result = new List<Track>();
                foreach (var track in e.Tracks)
                    result.Add(track.AsTrackWithSessions());

                return result;
            }
        }

        public void DeleteMyAgendaItem(int sessionid, int currentUserId)
        {
            try
            {
                using (var db = new CC.Data.CCDB())
                {
                    var sessionAttendee =
                        db.SessionAttendees.First(sa => sa.Session_ID == sessionid && sa.Person_ID == currentUserId);
                    if (sessionAttendee == null)
                    {
                        throw new ArgumentException("Agenda item not found");
                    }
                    db.SessionAttendees.Remove(sessionAttendee);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Agenda item not found");
            }
        }

        public void RateSession(SessionAttendee s)
        {

        }

        public IList<SessionAttendee> GetAttendedSessions(int eventId, int personId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var q = (from sa in db.SessionAttendees.Include("Session.Event").Include("Session.Timeslot").Include("Session.Speaker")
                         where sa.Person_ID == personId && sa.Session.Event_ID == eventId
                         orderby sa.Session.Timeslot.StartTime
                         select sa).ToList();

                List<SessionAttendee> result = new List<SessionAttendee>();
                foreach (var sa in q)
                    result.Add(sa.Map());
                return result;
            }
        }

        public IList<Session> GetMyAgenda(int eventId, int personId)
        {
            using (var db = new CC.Data.CCDB())
            {

                var q = (from sa in db.SessionAttendees.Include("Session.Event").Include("Session.Timeslot").Include("Session.Speaker")
                         where sa.Person_ID == personId && sa.Session.Event_ID == eventId
                         && sa.Session.Status == "APPROVED"
                         orderby sa.Session.Timeslot.StartTime
                         select sa).ToList();

                List<Session> result = new List<Session>();

                foreach (var sa in q)
                    result.Add(sa.Session.Map());

                return result;
            }
        }

        public void AttendSession(int personId, int sessionId)
        {
            using (var db = new CC.Data.CCDB())
            {

                // 1. delete old one
                Data.Session newSession = db.Sessions.Find(sessionId);
                if (newSession == null) throw new Exception("Session not found");

                var oldSession = (from sa in db.SessionAttendees.Include("Session.Timeslot")
                                  where sa.Person_ID == personId && sa.Session.ID == sessionId
                                  select sa).SingleOrDefault();

                //if (oldSession != null)
                //    db.SessionAttendees.Remove(oldSession);

                if (oldSession == null)
                    // 2. insert new one
                    db.SessionAttendees.Add(new CC.Data.SessionAttendee() { Person_ID = personId, Session_ID = sessionId, Comment = string.Empty, SessionRating = 0, SpeakerRating = 0 });

                db.SaveChanges();
            }
        }

        #endregion

        #region Stats

        public int GetTracksCount(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var e = db.Events.Find(eventId);

                if (e == null)
                    throw new ArgumentException("Event not found");

                return e.Tracks.Count();
            }
        }

        public int GetSessionsCount(int eventId)
        {
            var eventInfo = GetEvent(eventId);

            using (var db = new CC.Data.CCDB())
            {
                var sessions = (from session in db.Sessions
                                where session.Event_ID == eventId
                                select session);

                if (!eventInfo.IsSpeakerRegistrationOpen)
                {
                    sessions = sessions.Where(s => s.Status == ApprovedSession);
                }

                return sessions.Count();
            }
        }

        public int GetSpeakersCount(int eventId)
        {
            var eventInfo = GetEvent(eventId);

            using (var db = new CC.Data.CCDB())
            {
                var sessions = (from session in db.Sessions.Include("Speakers")
                                where session.Event_ID == eventId &&
                                ((!eventInfo.IsSpeakerRegistrationOpen && session.Status == ApprovedSession)
                                || (eventInfo.IsSpeakerRegistrationOpen))
                                select session);

                if (!eventInfo.IsSpeakerRegistrationOpen)
                {
                    sessions = sessions.Where(s => s.Status == ApprovedSession);
                }

                return sessions.Select(s => s.Speaker).Distinct().Count();
            }
        }

        public int GetAttendeesCount(int eventId)
        {
            using (var db = new CC.Data.CCDB())
            {
                var attendees = db.EventAttendees
                    .Where(x => x.Event_ID == eventId && x.Rsvp == "Yes")
                    .ToList();

                return attendees.Count();
            }
        }

        #endregion

        #region Tags

        public IList<Tag> GetTags()
        {
            return _tagRepository.GetTags();
        }

        public void AddTag(Tag tag)
        {
            _tagRepository.CreateTag(tag);
        }

        public Tuple<bool, string> DeleteTag(int tagId)
        {
            return _tagRepository.DeleteTag(tagId);
        }

        public void EditTag(Tag tag)
        {
            _tagRepository.UpdateTag(tag);
        }

        #endregion

        public string GetValueForKey(string key)
        {
            return _metadataRepository.GetValueForKey(key);
        }

        public async Tasks.Task<string> GetKey()
        {
            await Init();
            //return new Tasks.Task<string>(AKV.EncryptSecret);
            return AKV.EncryptSecret;
        }
    }
}