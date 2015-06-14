namespace OCC.Service.Webhost.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://onetug.org/2012/CodeCampService")]
    public interface ICodeCampService
    {
        #region People

        [OperationContract]
        int RegisterPerson(Person person);

        [OperationContract]
        Person Login(Person person);

        [OperationContract]
        void ChangePassword(int personId, string oldPassword, string newPassword);

        [OperationContract]
        void ResetPassword(string emailAddress, string temporaryPassword, string temporaryPasswordHash);

        [OperationContract]
        void UpdatePerson(Person person);

        [OperationContract]
        void DeletePerson(int personId);

        [OperationContract]
        Person FindPersonByEmail(string email);

        [OperationContract]
        IList<Person> GetAdministrators();

        [OperationContract]
        void AddAdministrator(int personId);

        [OperationContract]
        void RemoveAdministrator(int personId);

        [OperationContract]
        IList<Person> GetAllAttendees(int eventId);

        #endregion

        #region Events

        [OperationContract]
        IList<Event> GetEvents();

        [OperationContract]
        IList<Event> GetEventsByDate(DateTime fromDate, DateTime toDate);

        [OperationContract]
        void CreateEvent(Event _event);
        
        [OperationContract]
        Event GetEvent(int idEvent);

        [OperationContract]
        void UpdateEvent(Event _event);

        [OperationContract]
        Event GetDefaultEvent();

        #endregion

        #region Sponsors

        [OperationContract]
        IList<Sponsor> GetSponsors(int idEvent);

        [OperationContract]
        Sponsor GetSponsor(int id);

        [OperationContract]
        void CreateSponsor(Sponsor sponsor);

        [OperationContract]
        void UpdateSponsor(Sponsor sponsor);

        [OperationContract]
        void DeleteSponsor(int id);

        #endregion

        #region Announcements

        [OperationContract]
        IList<Announcement> GetAnnouncements(int idEvent);

        [OperationContract]
        IList<Announcement> GetCurrentAnnouncements(int idEvent);

        [OperationContract]
        Announcement GetAnnouncement(int id);

        [OperationContract]
        void CreateAnnouncement(Announcement sponsor);

        [OperationContract]
        void UpdateAnnouncement(Announcement sponsor);

        [OperationContract]
        void DeleteAnnouncement(int id);

        #endregion

        #region Tracks

        [OperationContract]
        IList<Track> GetTracks(int idEvent);

        [OperationContract]
        Track GetTrack(int id);

        [OperationContract]
        void CreateTrack(Track track);

        [OperationContract]
        void UpdateTrack(Track track);

        [OperationContract]
        void DeleteTrack(int id);

        #endregion

        #region Timeslots

        [OperationContract]
        IList<Timeslot> GetTimeslots(int idEvent);

        [OperationContract]
        Timeslot GetTimeslot(int id);

        [OperationContract]
        void CreateTimeslot(Timeslot Timeslot);

        [OperationContract]
        void UpdateTimeslot(Timeslot Timeslot);

        [OperationContract]
        void DeleteTimeslot(int id);

        #endregion

        #region Sessions

        [OperationContract]
        bool HasSubmittedRating(int personid, int eventid);

        [OperationContract]
        void CreateRateSession(Rate rating);

        [OperationContract]
        IList<Tag> GetTagsByEvent(int eventId);

        [OperationContract]
        IList<Session> GetSessions(int eventId);

        [OperationContract]
        IList<Session> GetApprovedSessions(int eventId);

        [OperationContract]
        Session GetSession(int id);

        [OperationContract]
        IList<Session> GetSpeakerSessions(int eventId, int speakerId);

        [OperationContract]
        void CreateSession(Session session);

        [OperationContract]
        void UpdateSession(Session session);

        [OperationContract]
        void DeleteSession(int id);

        #endregion

        #region Speakers

        [OperationContract]
        IList<Speaker> GetSpeakers(int eventId);

        [OperationContract]
        Speaker GetSpeaker(int eventId, int speakerId);

        #endregion

        #region Volunteers

        [OperationContract]
        IList<Person> GetTaskAssignees(int taskId);

        [OperationContract]
        IList<Task> GetAllCurrentEventTasks(int eventId);

        [OperationContract]
        void AssignTaskToPerson(Task task);

        [OperationContract]
        void RemoveTaskFromPerson(Task task);

        [OperationContract]
        Task GetTaskById(int taskId);

        [OperationContract]
        void AddTaskToEvent(Task newTask);

        [OperationContract]
        void DisableTask(int existingTaskId);

        [OperationContract]
        void UpdateTask(Task existingTask);

        #endregion

        #region EventAttendee

        [OperationContract]
        string GetEventAttendee(int eventId, int personId);

        [OperationContract]
        void Rsvp(int eventId, int personId, string rsvp);

        #endregion

        #region Schedule

        [OperationContract]
        void Schedule(int sessionId, int trackId, int timeslotId);

        #endregion

        #region Agenda

        [OperationContract]
        IList<Track> GetAgenda(int eventId);

        [OperationContract]
        IList<Session> GetMyAgenda(int eventId, int personId);

        [OperationContract]
        void RateSession(SessionAttendee s);

        [OperationContract]
        IList<SessionAttendee> GetAttendedSessions(int eventId, int personId);

        [OperationContract]
        void AttendSession(int personId, int sessionId);

        #endregion

        #region Stats

        [OperationContract]
        int GetTracksCount(int eventId);

        [OperationContract]
        int GetSessionsCount(int eventId);

        [OperationContract]
        int GetSpeakersCount(int eventId);

        [OperationContract]
        int GetAttendeesCount(int eventId);

        #endregion

        #region Tags

        [OperationContract]
        IList<Tag> GetTags();

        [OperationContract]
        void AddTag(Tag tag);

        [OperationContract]
        Tuple<bool, string> DeleteTag(int tagId);

        [OperationContract]
        void EditTag(Tag tag);

        #endregion

        [OperationContract]
        string GetValueForKey(string key);
    }
}