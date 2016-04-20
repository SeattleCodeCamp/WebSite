using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.UI.Webhost.Models;

namespace CC.UI.Webhost.Controllers
{
    public class TrackController : BaseController
    {
        //
        // GET: /Track/

        public ActionResult Index(int eventid)
        {
            var tracks = service.GetTracks(eventid);

            List<Track> model = new List<Track>();
            foreach (var track in tracks)
                model.Add(new Track() 
                { 
                    ID = track.ID, 
                    EventID = track.EventID,
                    Name = track.Name, 
                    Description = track.Description 
                });

            return View(model);
        }

        //
        // GET: /Track/Details/5

        public ActionResult Details(int id)
        {
            var track = service.GetTrack(id);

            Track model = new Track() 
            { 
                ID = track.ID, 
                EventID = track.EventID,
                Name = track.Name, 
                Description = track.Description 
            };

            return View(model);
        }

        //
        // GET: /Track/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create(int eventid)
        {
            Track model = new Track() { EventID = eventid };

            return View(model);
        } 

        //
        // POST: /Track/Create

        [HttpPost]
        public ActionResult Create(Track track) // (FormCollection collection)
        {
            try
            {
                service.CreateTrack(new CC.Service.Webhost.Services.Track()
                    {
                        EventID = track.EventID,
                        Name = track.Name,
                        Description = track.Description
                    });

                return RedirectToAction("Index");
            }
            catch
            {
                return View(track);
            }
        }
        
        //
        // GET: /Track/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var track = service.GetTrack(id);

            Track model = new Track() 
            { 
                ID = track.ID, 
                EventID = track.EventID, 
                Name = track.Name, 
                Description = track.Description 
            };

            return View(model);
        }

        //
        // POST: /Track/Edit/5

        [HttpPost]
        public ActionResult Edit(Track track) // (int id, FormCollection collection)
        {
            try
            {
                service.UpdateTrack(new CC.Service.Webhost.Services.Track()
                    {
                        ID = track.ID,
                        EventID = track.EventID,
                        Name = track.Name,
                        Description = track.Description
                    });
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Track/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeleteTrack(id);

                return RedirectToAction("Index");
            }
            catch // (Exception e)
            {
                ViewBag.ErrorMessage = "This track can not be deleted";

                var track = service.GetTrack(id);

                Track model = new Track() 
                { 
                    ID = track.ID, 
                    EventID = track.EventID,
                    Name = track.Name, 
                    Description = track.Description 
                };
                
                return View("Details", model);
            }
        }
    }
}