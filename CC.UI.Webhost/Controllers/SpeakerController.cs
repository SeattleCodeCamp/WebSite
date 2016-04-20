namespace CC.UI.Webhost.Controllers
{
    using System.Web.Mvc;

    using CC.UI.Webhost.Models;
using System.Web.UI;

    public class SpeakerController : BaseController
    {
        //
        // GET: /Speaker/

        public ActionResult Index()
        {
            return View("Details");
        }

        //
        // GET: /Speaker/Details/5

        [OutputCache(Duration = 600, Location=OutputCacheLocation.Client, VaryByParam = "id")]
        public ActionResult Details(int eventid, int id)
        {
            var speaker = service.GetSpeaker(eventid, id);

            Speaker model = new Speaker()
            {
                ID = speaker.ID,
                Email = speaker.Email,
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                Title = speaker.Title,
                Bio = speaker.Bio,
                Website = speaker.Website,
                Blog = speaker.Blog,
                Twitter = speaker.Twitter,
                ImageUrl = speaker.ImageUrl
            };

            foreach (var session in speaker.Sessions)
            {
                if (session.Status == "APPROVED")
                {
                    model.Sessions.Add(new Session()
                    {
                        ID = session.ID,
                        Name = session.Name,
                        Description = session.Description,
                        StartTime = session.StartTime.Value.ToShortTimeString(),
                        EndTime = session.EndTime.Value.ToShortTimeString(),
                        Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                    });
                }
            }

            return View(model);
        }

        //
        // GET: /Speaker/Create

        public ActionResult Create()
        {
            return View("Details");
        } 

        //
        // POST: /Speaker/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Details");
            }
        }
        
        //
        // GET: /Speaker/Edit/5
 
        public ActionResult Edit(int id)
        {
            // TODO: var speaker = service.GetSpeaker(id);
            // TODO: return View(speaker);

            return View("Details");
        }

        //
        // POST: /Speaker/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Details");
            }
        }

        //
        // GET: /Speaker/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View("Details");
        }

        //
        // POST: /Speaker/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Details");
            }
        }
    }
}
