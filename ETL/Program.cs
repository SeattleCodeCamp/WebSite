using System.Linq;
//using Person = CC.Service.Webhost.Services.Person;
using CC.Data;
using CC.Service.Webhost.Repositories;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace ETL
{
    class Program
    {
        private const bool DryRunOnly = false;
        private const int EventId = 3;
        private const string SourceFile = @"D:\src\speakers.json";
        private const string SourceFileSessions = @"D:\src\sessions.json";
        private static readonly PersonRepository _personRepository = new PersonRepository();
        private static readonly SessionRepository _sessionRepository = new SessionRepository();
        private static readonly EventRepository _eventRepository = new EventRepository();

        static void Main(string[] args)
        {
            VerifyEvent();

            var deserializedeSessions = Deserialize<AllSessions>(SourceFileSessions).First().sessions;
            deserializedeSessions.Select(l => l.categories.First().categoryItems.First().name);

            var deserializedeSpeakers = Deserialize<JsonTypes>(SourceFile);
            var people = _personRepository.FindAllPersons();
            var speakers = GetSpeakers(deserializedeSpeakers, deserializedeSessions);
            var (existingSpeakers, newSpeakers) = GetExistingSpeakers(people, speakers);

            Console.WriteLine($"Existing speakers: {existingSpeakers.Count}");
            Console.WriteLine($"New speakers: {newSpeakers.Count}");
            Console.WriteLine($"Total speakers: {existingSpeakers.Count + newSpeakers.Count}");

            if (!DryRunOnly)
            {
                InsertNewSpeakersAndSessions(newSpeakers);
                UpsertSessionsForExistingSpeakers(existingSpeakers);
            }
        }

        private static void VerifyEvent()
        {
            var events = _eventRepository.GetEvents();

            if (events.Where(e => e.IsDefault).OrderByDescending(o => o.ID).First().ID != EventId)
            {
                throw new Exception("You are trying to update non-default event. Are you sure?");
            }

            if (events.OrderByDescending(o => o.ID).First().ID != EventId)
            {
                throw new Exception("The event does not exist in the database. You are trying to add records for the event that does not exist.");
            }
        }

        private static void UpsertSessionsForExistingSpeakers(List<Person> existingSpeakers)
        {
            existingSpeakers.ForEach(speaker =>
            {
                Console.WriteLine($"Upserting {speaker.Sessions.Count} for {speaker.LastName}");

                speaker.Sessions.ForEach(session =>
                {
                    UpsertSessionForSpeaker(speaker.ID, session);
                });
            });
        }

        private static void UpsertSessionForSpeaker(int speakerId, CC.Data.Session session)
        {
            session.Speaker_ID = speakerId;
            var existingSession = _sessionRepository.GetSpeakerSessions(speakerId).Where(s => s.Name == session.Name && s.Event_ID == EventId).FirstOrDefault();

            if (existingSession == null)
            {
                Console.WriteLine($"'{session.Name}' session does not exist. Going to create it.");

                _sessionRepository.CreateSession(session);
                Console.WriteLine($"'{session.Name}' created.");
            }
            else
            {
                Console.WriteLine($"'{session.Name}' already exist. Going to update it.");
                _sessionRepository.UpdateSession(existingSession.ID, session);
            }
        }

        private static void InsertNewSpeakersAndSessions(List<Person> newSpeakers)
        {
            newSpeakers.ForEach(speaker =>
            {
                var sessions = speaker.Sessions;
                speaker.Sessions = null;
                Console.WriteLine($"Creating a speaker '{speaker.LastName}'.");
                var speakerId = _personRepository.RegisterPerson(speaker);

                sessions.ForEach(session =>
                {
                    session.Speaker_ID = speakerId;
                    Console.WriteLine($"Creating session '{session.Name}' for '{speaker.LastName}'.");
                    _sessionRepository.CreateSession(session);
                    Console.WriteLine($"'{session.Name}' session created for '{speaker.LastName}'.");
                });
            });
        }

        private static (List<Person>, List<Person>) GetExistingSpeakers(IEnumerable<Person> persons, IEnumerable<Person> speakers)
        {
            List<Person> existingSpeakers = new List<Person>();
            List<Person> newSpeakers = new List<Person>();

            foreach (var speaker in speakers)
            {
                var existingSpeaker = persons
                    .Where(person => person.LastName == speaker.LastName && person.FirstName == speaker.FirstName)
                    .OrderBy(o => o.ID)
                    .FirstOrDefault();

                if (existingSpeaker == null)
                {
                    Console.WriteLine($"'{speaker.LastName}'and '{speaker.FirstName}' is a new speaker.");
                    newSpeakers.Add(speaker);
                }
                else
                {
                    Console.WriteLine($"'{speaker.LastName}'and '{speaker.FirstName}' is an existing speaker.");
                    existingSpeaker.Sessions = speaker.Sessions;
                    existingSpeakers.Add(existingSpeaker);
                }
            }

            return (existingSpeakers, newSpeakers);
        }


        private static IEnumerable<Person> GetSpeakers(IEnumerable<JsonTypes> regSpeakers, Session2[] sessions)
        {
            var speakers = regSpeakers.Select(s => new Person
            {
                FirstName = s.firstName,
                LastName = s.lastName,
                Title = s.tagLine,
                Bio = s.bio,
                ImageUrl = s.profilePicture,
                Website = s.links.Where(l => l.linkType == "LinkedIn").FirstOrDefault() == null ? "" : s.links.Where(l => l.linkType == "LinkedIn").FirstOrDefault().url,
                Blog = s.links.Where(l => l.linkType == "Blog").FirstOrDefault() == null ? "" : s.links.Where(l => l.linkType == "Blog").FirstOrDefault().url,
                Twitter = s.links.Where(l => l.linkType == "Twitter").FirstOrDefault() == null ? "" : s.links.Where(l => l.linkType == "Twitter").FirstOrDefault().url,
                Sessions = GetSessions(s.sessions, sessions),
                TShirtSize = 0
            });

            return speakers;
        }

        private static List<CC.Data.Session> GetSessions(Session[] theSessoins, Session2[] allSessions)
        {
            var sessions = new List<CC.Data.Session>();

            foreach (var thes in theSessoins)
            {
                foreach (var alls in allSessions)
                {
                    if (thes.id.ToString() == alls.id)
                    {
                        sessions.Add(new CC.Data.Session
                        {
                            Event_ID = EventId,
                            Name = alls.title,
                            Description = alls.description.Substring(0, alls.description.Length < 2000 ? alls.description.Length : 2000),
                            Status = "APPROVED",
                            Level = Getlevel(alls.categories.Select(l => l.categoryItems.First().name).First()),
                            Timeslot_ID = 1
                        });

                        break;
                    }
                }
            }

            return sessions;
        }

        private static int Getlevel(string v)
        {
            switch (v)
            {
                case "Overview":
                    return 100;
                case "Introductory":
                    return 200;
                case "Intermediate":
                    return 200;
                case "NoLevel":
                    return 100;
                default:
                    return 100;
            }
        }

        private static IList<T> Deserialize<T>(string path)
        {
            var json = File.ReadAllText(path);
            var res = JsonConvert.DeserializeObject<List<T>>(json);
            return res;
        }

    }
}
