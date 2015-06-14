namespace OCC.UI.Webhost.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using OCC.UI.Webhost.Models;
    using System;
    using System.Web.UI;

    public class SessionController : BaseController
    {
        //
        // GET: /Session/

        public ActionResult Index()
        {
            return View("Details");
        }

        //
        // GET: //SpeakerSessions/

        [Authorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Client, VaryByParam = "eventid")]
        public ActionResult SpeakerSessions(int eventid)
        {
            var sessions = service.GetSpeakerSessions(eventid, this.CurrentUser.ID);

            List<Session> model = new List<Session>();
            foreach (var session in sessions)
                model.Add(new Session()
                {
                    ID = session.ID,
                    Name = session.Name,
                    Description = session.Description,
                    Level = session.Level.ToString(),
                    Status = session.Status,
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                });

            return View(model);
        }

        //
        // GET: /Session/Details/5

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public ActionResult Details(int id)
        {
            var session = service.GetSession(id);

            Session model = new Session()
            {
                ID = session.ID,
                EventID = session.EventID,
                SpeakerID = session.SpeakerID,
                Name = session.Name,
                Description = session.Description,
                Level = session.Level.ToString(),
                Speaker = session.Speaker,
                Status = session.Status,
                ImageUrl = session.ImageUrl,
                StartTime = string.Format("{0}", session.StartTime.HasValue ? session.StartTime.Value.ToShortTimeString() : "N/A"),
                EndTime = string.Format("{0}", session.EndTime.HasValue ? session.EndTime.Value.ToShortTimeString() : "N/A"),
                Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
            };

            return View(model);
        }

        [Authorize]
        public ActionResult Attend(int id)
        {
            service.AttendSession(CurrentUser.ID, id);

            return RedirectToAction("MyAgenda", "Home");
        }

        //
        // GET: /Session/Create

        [Authorize]
        public ActionResult Create(int eventid)
        {
            var currentEvent = service.GetEvent(eventid);

            ViewBag.Event = currentEvent;
            var result = service.GetTags();
            ViewBag.Tags = result;
            return View("Create");
        }

        //
        // POST: /Session/Create

        [HttpPost]
        public ActionResult Create(int eventid, Session session)
        {
            ViewBag.Event = service.GetEvent(eventid);

            try
            {
                service.CreateSession(new CodeCampService.Session()
                {
                    EventID = eventid,
                    SpeakerID = this.CurrentUser.ID,
                    Name = session.Name,
                    Description = session.Description,
                    Level = Int32.Parse(session.Level),
                    TagID = session.TagID.Value,
                    Status = "SUBMITTED",
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                });

                return RedirectToAction("SpeakerSessions");
            }
            catch(Exception ex)
            {
                return View(session);
            }
        }

        //
        // GET: /Session/Edit/5

        [Authorize]
        public ActionResult Edit(int id)
        {
            var session = service.GetSession(id);
            var result = service.GetTags();
            ViewBag.Tags = result;

            if (session.SpeakerID == CurrentUser.ID)
            {
                Session model = new Session()
                {
                    ID = session.ID,
                    EventID = session.EventID,
                    SpeakerID = session.SpeakerID,
                    Name = session.Name,
                    Description = session.Description,
                    Level = session.Level.ToString(),
                    Speaker = session.Speaker,
                    Status = session.Status,
                    TagID = session.TagID,
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("SpeakerSessions");
            }
        }

        //
        // POST: /Session/Edit/5

        [HttpPost]
        public ActionResult Edit(Session session)
        {
            try
            {
                var savedSession = service.GetSession(session.ID);
                savedSession.Name = session.Name;
                savedSession.Description = session.Description;
                savedSession.Level = Int32.Parse(session.Level);
                savedSession.TagID = session.TagID.Value;
                service.UpdateSession(savedSession);

                return RedirectToAction("SpeakerSessions");
            }
            catch
            {
                return View(session);
            }

        }

        //
        // GET: /Session/Delete/5

        //[Authorize]
        //public ActionResult Delete(int id)
        //{
        //    var session = service.GetSession(id);

        //    if (session.SpeakerID == CurrentUser.ID)
        //    {
        //        Session model = new Session()
        //        {
        //            ID = session.ID,
        //            EventID = session.EventID,
        //            SpeakerID = session.SpeakerID,
        //            Name = session.Name,
        //            Description = session.Description,
        //            Level = session.Level.ToString(),
        //            Speaker = session.Speaker,
        //            Status = session.Status
        //        };

        //        return View(model);
        //    }
        //    else
        //    {
        //        return RedirectToAction("SpeakerSessions");
        //    }
        //}

        //
        // POST: /Session/Delete/5

        // [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                service.DeleteSession(id);

                return RedirectToAction("SpeakerSessions");
            }
            catch
            {
                var session = service.GetSession(id);

                Session model = new Session()
                {
                    ID = session.ID,
                    EventID = session.EventID,
                    SpeakerID = session.SpeakerID,
                    Name = session.Name,
                    Description = session.Description,
                    Level = session.Level.ToString(),
                    Speaker = session.Speaker,
                    Status = session.Status,
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                };

                return View("Details", model);
            }
        }
    }
}