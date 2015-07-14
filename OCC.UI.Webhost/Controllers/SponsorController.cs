namespace OCC.UI.Webhost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using OCC.UI.Webhost.Models;

    public class SponsorController : BaseController
    {
        //
        // GET: /Sponsor/

        public ActionResult Index(int eventid)
        {
            var sponsors = service.GetSponsors(eventid);

            List<Sponsor> model = new List<Sponsor>();

            foreach (var sponsor in sponsors)
                model.Add(new Sponsor()
                {
                    ID = sponsor.ID,
                    EventID = sponsor.EventID,
                    Name = sponsor.Name,
                    Description = sponsor.Description,
                    SponsorshipLevel = sponsor.SponsorshipLevel,
                    WebsiteUrl = sponsor.WebsiteUrl,
                    ImageUrl = sponsor.ImageUrl,
                    Logo = sponsor.Image == null ? null : new Infrastructure.WebImageOCC(sponsor.Image),
                });

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
                ImageUrl = sponsor.ImageUrl
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
                service.CreateSponsor(new CodeCampService.Sponsor()
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
            catch (Exception ex)
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

            Sponsor model = new Sponsor()
            {
                ID = sponsor.ID,
                EventID = sponsor.EventID,
                Name = sponsor.Name,
                Description = sponsor.Description,
                SponsorshipLevel = sponsor.SponsorshipLevel,
                WebsiteUrl = sponsor.WebsiteUrl,
                Logo = sponsor.Image == null ? null : new Infrastructure.WebImageOCC(sponsor.Image),
            };

            return View(model);
        }

        //
        // POST: /Sponsor/Edit/5

        [HttpPost]
        public ActionResult Edit(Sponsor sponsor)
        {
            try
            {
                service.UpdateSponsor(new CodeCampService.Sponsor()
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
                    ImageUrl = sponsor.ImageUrl
                };

                return View("Details", model);
            }
        }

        private byte[] ConvertToByes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}
