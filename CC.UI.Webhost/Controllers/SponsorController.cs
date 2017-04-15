using CC.Service.Webhost.CodeCampSvc;

namespace CC.UI.Webhost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using CC.UI.Webhost.Models;
    using OCC.UI.Webhost.Utilities;
    using System.Linq;

    public class SponsorController : BaseController
    {

        public SponsorController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo) { }


        //
        // GET: /Sponsor/

        public ActionResult Index(int eventid)
        {
            var sponsors = service.GetSponsors(eventid);
            List<Sponsor> model = sponsors.Select(ServiceToWebHostSponsor).ToList();
            return View(model);
        }

        //
        // GET: /Sponsor/Details/5

        public ActionResult Details(int id)
        {
            var sponsor = service.GetSponsor(id);

            Sponsor model = new Sponsor()
            {
                ID = sponsor.ID,
                EventID = sponsor.EventID,
                Name = sponsor.Name,
                Description = sponsor.Description,
                SponsorshipLevel = sponsor.SponsorshipLevel,
                WebsiteUrl = sponsor.WebsiteUrl,
            };

            return View(model);
        }

        //
        // GET: /Sponsor/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create(int eventid)
        {
            Sponsor model = new Sponsor() { EventID = eventid };

            return View(model);
        }

        //
        // POST: /Sponsor/Create

        [HttpPost]
        public ActionResult Create(Sponsor sponsor)
        {
            try
            {
                service.CreateSponsor(new CC.Service.Webhost.Services.Sponsor()
                {
                    EventID = sponsor.EventID,
                    Name = sponsor.Name,
                    Description = sponsor.Description,
                    SponsorshipLevel = sponsor.SponsorshipLevel,
                    WebsiteUrl = sponsor.WebsiteUrl,
                    Image = ConvertToByes(Request.Files["Logo"])
                });

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(sponsor);
            }
        }

        //
        // GET: /Sponsor/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var sponsor = service.GetSponsor(id);
            Sponsor model = ServiceToWebHostSponsor(sponsor);
            return View(model);
        }

        //
        // POST: /Sponsor/Edit/5

        [HttpPost]
        public ActionResult Edit(Sponsor sponsor)
        {
            try
            {
                service.UpdateSponsor(new CC.Service.Webhost.Services.Sponsor()
                {
                    ID = sponsor.ID,
                    EventID = sponsor.EventID,
                    Name = sponsor.Name,
                    Description = sponsor.Description,
                    SponsorshipLevel = sponsor.SponsorshipLevel,
                    WebsiteUrl = sponsor.WebsiteUrl,
                    Image = ConvertToByes(Request.Files["Logo"])
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Sponsor/Delete/5

        // [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.DeleteSponsor(id);

                return RedirectToAction("Index");
            }
            catch
            {
                var sponsor = service.GetSponsor(id);

                Sponsor model = new Sponsor()
                {
                    ID = sponsor.ID,
                    EventID = sponsor.EventID,
                    Name = sponsor.Name,
                    Description = sponsor.Description,
                    SponsorshipLevel = sponsor.SponsorshipLevel,
                    WebsiteUrl = sponsor.WebsiteUrl,
                };

                return View("Details", model);
            }
        }

        #region Private Helpers

        private byte[] ConvertToByes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        // Leaving fully qualified names here since it's kind of confusing having the two sponsor classes
        // named the same.  This way, it's clear.
        private CC.UI.Webhost.Models.Sponsor ServiceToWebHostSponsor(Service.Webhost.Services.Sponsor serviceSponsor)
        {
            return new CC.UI.Webhost.Models.Sponsor()
            {
                ID = serviceSponsor.ID,
                EventID = serviceSponsor.EventID,
                Name = serviceSponsor.Name,
                Description = serviceSponsor.Description,
                SponsorshipLevel = serviceSponsor.SponsorshipLevel,
                WebsiteUrl = serviceSponsor.WebsiteUrl,
                Logo = ImageUtils.ImageFromBytes(serviceSponsor.Image),
            };
        }

        #endregion
    }
}
