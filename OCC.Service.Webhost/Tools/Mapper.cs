namespace OCC.Service.Webhost.Tools
{
    using System;
    using System.Reflection;

    using OCC.Service.Webhost.Services;
    using System.Linq;

    public static class Mapper
    {
        public static void CopyProperties(object from, object to)
        {
            foreach (PropertyInfo source in from.GetType().GetProperties())
            {
                PropertyInfo target = to.GetType().GetProperty(source.Name);

                if (target != null)
                {
                    if (source.PropertyType.FullName == target.PropertyType.FullName)
                    {
                        var v = source.GetValue(from, null);
                        target.SetValue(to, v, null);
                    }
                    else
                    {
                        var a = source.GetValue(from, null);
                        var b = target.GetValue(to, null);

                        CopyProperties(a, b);
                    }
                }
            }
        }

        public static Event Map(this Data.Event e)
        {
            Event result = new Event()
            {
                ID = e.ID,
                Name = e.Name,
                Description = e.Description,
                TwitterHashTag = e.TwitterHashTag,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Location = e.Location,
                Address1 = e.Address1,
                Address2 = e.Address2,
                City = e.City,
                State = e.State,
                Zip = e.Zip,
                IsDefault = e.IsDefault,
                IsSponsorRegistrationOpen = e.IsSponsorRegistrationOpen,
                IsSpeakerRegistrationOpen = e.IsSpeakerRegistrationOpen,
                IsAttendeeRegistrationOpen = e.IsAttendeeRegistrationOpen,
                IsVolunteerRegistrationOpen = e.IsVolunteerRegistrationOpen
            };

            return result;
        }


        public static Tag Map(this Data.Tag e, int count)
        {
            Tag result = new Tag()
            {
                ID = e.ID,
                TagName = e.TagName
            };
            result.SessionsCount = count;
            return result;
        }

        public static Track Map(this Data.Track t)
        {
            Track track = new Track()
            {
                ID = t.ID,
                EventID = t.Event_ID,
                Name = t.Name,
                Description = t.Description
            };

            return track;
        }

        public static Track AsTrackWithSessions(this Data.Track t)
        {
            Track track = new Track()
            {
                ID = t.ID,
                EventID = t.Event_ID,
                Name = t.Name,
                Description = t.Description
            };

            foreach (var sesion in t.Sessions)
                track.Sessions.Add(sesion.Map());

            return track;
        }

        public static Timeslot Map(this Data.Timeslot t)
        {
            Timeslot timeslot = new Timeslot()
            {
                ID = t.ID,
                EventID = t.Event_ID,
                Name = t.Name,
                StartTime = t.StartTime ?? DateTime.MinValue, // why am I doing this?
                EndTime = t.EndTime ?? DateTime.MinValue
            };

            return timeslot;
        }

        public static SessionAttendee Map(this Data.SessionAttendee s)
        {
            SessionAttendee attendee = new SessionAttendee()
            {
                ID = s.ID,
                SpeakerName = string.Format("{0} {1}", s.Session.Speaker.FirstName, s.Session.Speaker.LastName),
                SessionName = s.Session.Name,
                SessionRating = s.SessionRating,
                SpeakerRating = s.SpeakerRating,
                Comment = s.Comment
            };
            return attendee;
        }

        public static Session Map(this Data.Session s)
        {
            Session session = new Session()
            {
                ID = s.ID,
                Name = s.Name,
                Description = s.Description,
                Level = s.Level,
                Status = s.Status,
                Location = s.Location,
                Speaker = s.Speaker.FirstName + " " + s.Speaker.LastName,
                ImageUrl = s.Speaker.ImageUrl,
                SpeakerID = s.Speaker.ID,
                TrackID = s.Track_ID,
                Track = s.Track == null ? "" : s.Track.Name,
                TimeslotID = s.Timeslot_ID,
                StartTime = s.Timeslot == null ? null : s.Timeslot.StartTime,
                EndTime = s.Timeslot == null ? null : s.Timeslot.EndTime,
                TagID = s.Tag_ID,
                EventID = s.Event_ID
            };


            return session;
        }

        public static Speaker AsSpeaker(this Data.Person p)
        {
            Speaker speaker = new Speaker()
            {
                ID = p.ID,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Title = p.Title,
                Bio = p.Bio,
                Website = p.Website,
                Blog = p.Blog,
                Twitter = p.Twitter,
                ImageUrl = p.ImageUrl
            };

            foreach (var session in p.Sessions)
                speaker.Sessions.Add(session.Map());

            return speaker;
        }

        public static Data.Person Map(this Person p)
        {
            var person = new Data.Person()
            {
                ID = p.ID,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Title = p.Title,
                Bio = p.Bio,
                Website = p.Website,
                Blog = p.Blog,
                Twitter = p.Twitter,
                ImageUrl = p.ImageUrl
            };

            return person;
        }

        public static Person Map(this Data.Person p)
        {
            Person person = new Person
            {
                ID = p.ID,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Title = p.Title,
                Bio = p.Bio,
                Website = p.Website,
                Blog = p.Blog,
                Twitter = p.Twitter,
                ImageUrl = p.ImageUrl
            };

            return person;
        }

        public static Data.Announcement Map(this Announcement a)
        {
            Data.Announcement announcement = new Data.Announcement
            {
                ID = a.ID,
                Event_ID = a.EventID,
                Title = a.Title,
                Content = a.Content,
                PublishDate = a.PublishDate
            };

            return announcement;
        }

        public static Announcement Map(this Data.Announcement a)
        {
            Announcement announcement = new Announcement
            {
                ID = a.ID,
                EventID = a.Event_ID,
                Title = a.Title,
                Content = a.Content,
                PublishDate = a.PublishDate
            };

            return announcement;
        }

        public static Data.Sponsor Map(this Sponsor s)
        {
            Data.Sponsor sponsor = new Data.Sponsor
            {
                ID = s.ID,
                Event_ID = s.EventID,
                Name = s.Name,
                Description = s.Description,
                WebsiteUrl = s.WebsiteUrl,
                SponsorshipLevel = s.SponsorshipLevel,
                ImageUrl = s.ImageUrl,
                Image = s.Image
            };

            return sponsor;
        }

        public static Sponsor Map(this Data.Sponsor s)
        {
            Sponsor sponsor = new Sponsor
            {
                ID = s.ID,
                EventID = s.Event_ID,
                Name = s.Name,
                Description = s.Description,
                WebsiteUrl = s.WebsiteUrl,
                SponsorshipLevel = s.SponsorshipLevel,
                ImageUrl = s.ImageUrl
            };

            return sponsor;
        }

        public static Task Map(this Data.Task task)
        {
            return new Task
            {
                Id = task.ID,
                Description = task.Description,
                Event = task.Event.Map(),
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                Capacity = task.Capacity
            };
        }

        public static Data.Task Map(this Task dcTask)
        {
            var task = new Data.Task
                           {
                               ID = dcTask.Id,
                               StartTime = dcTask.StartTime,
                               EndTime = dcTask.EndTime,
                               Capacity = dcTask.Capacity,
                               Description = dcTask.Description,
                               Event_ID = dcTask.EventID
                           };
            return task;
        }
    }
}