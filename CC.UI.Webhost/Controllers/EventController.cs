﻿using CC.Service.Webhost.CodeCampSvc;

namespace CC.UI.Webhost.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CC.UI.Webhost.Models;

    public class EventController : BaseController
    {
        public EventController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo) { }

        //
        // GET: /Event/

        public ActionResult Index()
        {
            var events = service.GetEvents();

            List<Event> model = new List<Event>();
            foreach (var e in events) model.Add(new Event() { ID = e.ID, Name = e.Name });

            return View(model);
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id)
        {
            var e = service.GetEvent(id);

            Event model = new Event()
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

            return View(model);
        }

        //
        // GET: /Event/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(Event e)//(FormCollection collection)
        {
            try
            {
                service.CreateEvent(new CC.Service.Webhost.Services.Event()
                {
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
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        //
        // GET: /Event/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var e = service.GetEvent(id);

            Event model = new Event()
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

            return View(model);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(Event e)// (int id, FormCollection collection)
        {
            try
            {
                service.UpdateEvent(new CC.Service.Webhost.Services.Event()
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
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View(e);
            }
        }

        //
        // GET: /Event/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: service.DeleteEvent();

                return RedirectToAction("Index");
            }
            catch
            {
                // TODO: error message

                return View("Index");
            }
        }
    }
}
