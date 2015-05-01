using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OCC.UI.Webhost.Models;

namespace OCC.UI.Webhost.Controllers
{
    public class TimeslotController : BaseController
    {
        //
        // GET: /Timeslot/

        public ActionResult Index(int eventid)
        {
            var timeslots = service.GetTimeslots(eventid);

            List<Timeslot> model = new List<Timeslot>();
            foreach (var timeslot in timeslots)
                model.Add(new Timeslot()
                {
                    ID = timeslot.ID,
                    EventID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                });

            return View(model);
        }

        //
        // GET: /Timeslot/Details/5

        public ActionResult Details(int id)
        {
            var timeslot = service.GetTimeslot(id);

            Timeslot model = new Timeslot()
            {
                ID = timeslot.ID,
                EventID = timeslot.EventID,
                Name = timeslot.Name,
                StartTime = timeslot.StartTime,
                EndTime = timeslot.EndTime
            };

            return View(model);
        }

        //
        // GET: /Timeslot/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create(int eventid)
        {
            Timeslot model = new Timeslot() { EventID = eventid };

            return View(model);
        }

        //
        // POST: /Timeslot/Create

        [HttpPost]
        public ActionResult Create(Timeslot timeslot) // (FormCollection collection)
        {
            try
            {
                service.CreateTimeslot(new CodeCampService.Timeslot()
                {
                    EventID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View(timeslot);
            }
        }

        //
        // GET: /Timeslot/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var timeslot = service.GetTimeslot(id);

            Timeslot model = new Timeslot()
            {
                ID = timeslot.ID,
                EventID = timeslot.EventID,
                Name = timeslot.Name,
                StartTime = timeslot.StartTime,
                EndTime = timeslot.EndTime
            };

            return View(model);
        }

        //
        // POST: /Timeslot/Edit/5

        [HttpPost]
        public ActionResult Edit(Timeslot timeslot) // (int id, FormCollection collection)
        {
            try
            {
                service.UpdateTimeslot(new CodeCampService.Timeslot()
                {
                    ID = timeslot.ID,
                    EventID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Timeslot/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeleteTimeslot(id);

                return RedirectToAction("Index");
            }
            catch // (Exception e)
            {
                ViewBag.ErrorMessage = "This Timeslot can not be deleted";

                var timeslot = service.GetTimeslot(id);

                Timeslot model = new Timeslot()
                {
                    ID = timeslot.ID,
                    EventID = timeslot.EventID,
                    Name = timeslot.Name,
                    StartTime = timeslot.StartTime,
                    EndTime = timeslot.EndTime
                };

                return View("Details", model);
            }
        }
    }
}