using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Model = CC.UI.Webhost.Models;
using UiModel = CC.UI.Webhost.Models;
using Services = CC.Service.Webhost.Services;

namespace CC.UI.Webhost.Infrastructure
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Maps a CodeCampService.Person to a Model.Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static Model.Person Map(Services.Person person)
        {
            var p = new Model.Person
            {
                ID = person.ID,
                ImageUrl = person.ImageUrl,
                Website = person.Website,
                Email = person.Email,
                Bio = person.Bio,
                Twitter = person.Twitter,
                Blog = person.Blog,
                Title = person.Title,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsAdmin = person.IsAdmin,
                Location = person.Location,
                TShirtSizeId = person.TShirtSize
            };
            return p;
        }

        /// <summary>
        /// Maps a Model.Person to a CodeCampService.Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static Services.Person Map(this Model.Person person)
        {
            return new Services.Person
            {
                ID = person.ID,
                ImageUrl = person.ImageUrl,
                Website = person.Website,
                Email = person.Email,
                Bio = person.Bio,
                Twitter = person.Twitter,
                Blog = person.Blog,
                Title = person.Title,
                FirstName = person.FirstName, 
                LastName = person.LastName,
                IsAdmin = person.IsAdmin,
                Location = person.Location,
                TShirtSize = person.TShirtSizeId
            };
        }

        public static Services.Person Transform(this UiModel.Person person)
        {
            return new Services.Person
            {
                ID = person.ID,
                ImageUrl = person.ImageUrl,
                Website = person.Website,
                Email = person.Email,
                Bio = person.Bio,
                Twitter = person.Twitter,
                Blog = person.Blog,
                Title = person.Title,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsAdmin = person.IsAdmin,
                Location = person.Location
            };
        }

        /// <summary>
        /// Maps a CodeCampService.Speaker to a Model.Speaker
        /// </summary>
        /// <param name="speaker">CodeCampService.Speaker</param>
        /// <returns>Model.Speaker</returns>
        public static Model.Speaker Map(Services.Speaker speaker)
        {
            var modelSpeaker = new Model.Speaker()
                                   {
                                       ImageUrl = speaker.ImageUrl,
                                       Website = speaker.Website,
                                       Email = speaker.Email,
                                       Bio = speaker.Bio,
                                       Twitter = speaker.Twitter,
                                       Blog = speaker.Blog,
                                       Title = speaker.Title,
                                       FirstName = speaker.FirstName,
                                       LastName = speaker.LastName,
                                       IsAdmin = speaker.IsAdmin
                                   };

            modelSpeaker.Sessions = (List<Model.Session>)speaker.Sessions.Map();
            return modelSpeaker;
        }


        /// <summary>
        /// Maps a CodeCampService.Session to a Model.Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Model.Session Map(this Services.Session session)
        {
            var s = new Model.Session
            {
                ID = session.ID,
                EventID = session.EventID,
                Description = session.Description,
                Level = session.Level.ToString(CultureInfo.InvariantCulture),
                Name = session.Name,
                Speaker = session.Speaker,
                SpeakerID = session.SpeakerID,
                Status = session.Status,
                Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
            };
            return s;
        }

        public static IList<Model.Session> Map(this IList<Services.Session> sessions)
        {
            var modelSessionList = new List<Model.Session>();

            sessions.ToList().ForEach(s =>
                                          {
                                              var modelSession = s.Map();
                                              modelSessionList.Add(modelSession);
                                          });
            return modelSessionList;
        }

        public static IList<Services.Session> Map(this IList<Model.Session> sessions)
        {
            var serviceSessionList = new List<Services.Session>();

            sessions.ToList().ForEach(s =>
            {
                var serrviceSession = s.Map();
                serviceSessionList.Add(serrviceSession);
            });
            return serviceSessionList;
        }

        /// <summary>
        /// Maps a Model.Session to a CodeCampService.Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Services.Session Map(this Model.Session session)
        {
            int sessionLevel = 0;
            Int32.TryParse(session.Level, out sessionLevel);

            var s = new Services.Session
            {
                ID = session.ID,
                EventID = session.EventID,
                Description = session.Description,
                Level = sessionLevel,
                Name = session.Name,
                Speaker = session.Speaker,
                SpeakerID = session.SpeakerID,
                Status = session.Status,
                Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
            };
            return s;
        }

        /// <summary>
        /// Maps the roles for a Person based on the existence 
        /// of certain properties and flags
        /// </summary>
        /// <param name="p">Model.Person</param>
        /// <param name="speaker">Model.Speaker</param>
        public static void SetRolesForPerson(this Model.Person p, Services.Speaker speaker = null)
        {
            if (p != null)
            {
                if (p.IsAdmin)
                {
                    p.Roles.Add("Admin");
                }
            }

            if (speaker != null)
            {
                if (speaker.Sessions != null)
                {
                    if (speaker.Sessions.Any())
                    {
                        p.Roles.Add("Speaker");
                    }
                }
            }
        }
    }
}