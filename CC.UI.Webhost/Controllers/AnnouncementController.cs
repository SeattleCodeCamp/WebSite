using CC.Service.Webhost.CodeCampSvc;

namespace CC.UI.Webhost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CC.UI.Webhost.Models;

    public class AnnouncementController : BaseController
    {
        public AnnouncementController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo) { }


        //
        // GET: /Announcement/
        public ActionResult Index(int eventid)
        {
            var announcements = service.GetAnnouncements(eventid);

            List<Announcement> model = new List<Announcement>();

            foreach (var announcement in announcements)
                model.Add(new Announcement()
                {
                    ID = announcement.ID,
                    EventID = announcement.EventID,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    PublishDate = announcement.PublishDate
                });

            return View(model);
        }

        //
        // GET: /Announcement/Details/5

        public ActionResult Details(int id)
        {
            var announcement = service.GetAnnouncement(id);

            Announcement model = new Announcement()
            {
                ID = announcement.ID,
                EventID = announcement.EventID,
                Title = announcement.Title,
                Content = announcement.Content,
                PublishDate = announcement.PublishDate
            };

            return View(model);
        }

        //
        // GET: /Announcement/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int eventid)
        {
            Announcement model = new Announcement();

            model.EventID = eventid;
            model.PublishDate = DateTime.Today;

            return View(model);
        }

        //
        // POST: /Announcement/Create

        [HttpPost]
        public ActionResult Create(Announcement announcement)
        {
            try
            {
                service.CreateAnnouncement(new CC.Service.Webhost.Services.Announcement
                {
                    EventID = announcement.EventID,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    PublishDate = announcement.PublishDate
                });

                return RedirectToAction("Index");
            }
            catch
            {
                // TODO: show the error

                return View(announcement);
            }
        }

        //
        // GET: /Announcement/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var announcement = service.GetAnnouncement(id);

            Announcement model = new Announcement()
            {
                ID = announcement.ID,
                EventID = announcement.EventID,
                Title = announcement.Title,
                Content = announcement.Content,
                PublishDate = announcement.PublishDate
            };

            return View(model);
        }

        //
        // POST: /Announcement/Edit/5

        [HttpPost]
        public ActionResult Edit(Announcement announcement)
        {
            try
            {
                service.UpdateAnnouncement(new CC.Service.Webhost.Services.Announcement
                {
                    ID = announcement.ID,
                    EventID = announcement.EventID,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    PublishDate = announcement.PublishDate
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Announcement/Delete/5

        // [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeleteAnnouncement(id);

                return RedirectToAction("Index");
            }
            catch
            {
                var announcement = service.GetAnnouncement(id);

                Announcement model = new Announcement()
                {
                    ID = announcement.ID,
                    EventID = announcement.EventID,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    PublishDate = announcement.PublishDate
                };

                return View("Details", model);
            }
        }
    }
}