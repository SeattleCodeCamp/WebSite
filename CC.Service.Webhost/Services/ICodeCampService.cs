using System;
using System.Collections.Generic;
using CC.Service.Webhost.Services;

namespace CC.Service.Webhost.CodeCampSvc
{
    public interface ICodeCampService
    {
        #region People

        int RegisterPerson(Person person);

        Person Login(Person person);

        void ChangePassword(int personId, string oldPassword, string newPassword);

        void ResetPassword(string emailAddress, string temporaryPassword, string temporaryPasswordHash);

        void UpdatePerson(Person person);

        void DeletePerson(int personId);

        Person FindPersonByEmail(string email, string provider);

        Person FindPersonByEmail(string email);

        IList<Person> GetAdministrators();

        void AddAdministrator(int personId);

        void RemoveAdministrator(int personId);

        IList<Person> GetAllAttendees(int eventId);

        #endregion

        #region Events

        IList<Event> GetEvents();

        IList<Event> GetEventsByDate(DateTime fromDate, DateTime toDate);

        void CreateEvent(Event _event);
        
        Event GetEvent(int idEvent);

        void UpdateEvent(Event _event);

        Event GetDefaultEvent();

        #endregion

        #region Sponsors

        IList<Sponsor> GetSponsors(int idEvent);

        Sponsor GetSponsor(int id);

        void CreateSponsor(Sponsor sponsor);

        void UpdateSponsor(Sponsor sponsor);

        void DeleteSponsor(int id);

        #endregion

        #region Announcements

        IList<Announcement> GetAnnouncements(int idEvent);

        IList<Announcement> GetCurrentAnnouncements(int idEvent);

        Announcement GetAnnouncement(int id);

        void CreateAnnouncement(Announcement sponsor);

        void UpdateAnnouncement(Announcement sponsor);

        void DeleteAnnouncement(int id);

        #endregion

        #region Tracks

        IList<Track> GetTracks(int idEvent);

        Track GetTrack(int id);

        void CreateTrack(Track track);

        void UpdateTrack(Track track);

        void DeleteTrack(int id);

        #endregion

        #region Timeslots

        IList<Timeslot> GetTimeslots(int idEvent);

        Timeslot GetTimeslot(int id);

        void CreateTimeslot(Timeslot Timeslot);

        void UpdateTimeslot(Timeslot Timeslot);

        void DeleteTimeslot(int id);

        #endregion

        #region Sessions

        bool HasSubmittedRating(int personid, int eventid);

        void CreateRateSession(Rate rating);

        IList<Tag> GetTagsByEvent(int eventId);

        IList<Session> GetSessions(int eventId);

        IList<Session> GetApprovedSessions(int eventId);

        Session GetSession(int id);

        IList<Session> GetSpeakerSessions(int eventId, int speakerId);

        void CreateSession(Session session);

        void UpdateSession(Session session);

        void DeleteSession(int id);

        #endregion

        #region Speakers

        IList<Speaker> GetSpeakers(int eventId);

        Speaker GetSpeaker(int eventId, int speakerId);

        #endregion

        #region Volunteers

        IList<Person> GetTaskAssignees(int taskId);

        IList<Task> GetAllCurrentEventTasks(int eventId);

        void AssignTaskToPerson(Task task);

        void RemoveTaskFromPerson(Task task);

        Task GetTaskById(int taskId);

        void AddTaskToEvent(Task newTask);

        void DisableTask(int existingTaskId);

        void UpdateTask(Task existingTask);

        #endregion

        #region EventAttendee

        string GetEventAttendee(int eventId, int personId);

        void Rsvp(int eventId, int personId, string rsvp);

        #endregion

        #region Schedule

        void Schedule(int sessionId, int trackId, int timeslotId);

        #endregion

        #region Agenda

        IList<Track> GetAgenda(int eventId);

        IList<Session> GetMyAgenda(int eventId, int personId);

        void DeleteMyAgendaItem(int sessionid, int currentUserId);

        void RateSession(SessionAttendee s);

        IList<SessionAttendee> GetAttendedSessions(int eventId, int personId);

        void AttendSession(int personId, int sessionId);

        #endregion

        #region Stats

        int GetTracksCount(int eventId);

        int GetSessionsCount(int eventId);

        int GetSpeakersCount(int eventId);

        int GetAttendeesCount(int eventId);

        #endregion

        #region Tags

        IList<Tag> GetTags();

        void AddTag(Tag tag);

        Tuple<bool, string> DeleteTag(int tagId);

        void EditTag(Tag tag);

        #endregion

        string GetValueForKey(string key);
    }
}