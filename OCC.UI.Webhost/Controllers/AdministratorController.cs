namespace OCC.UI.Webhost.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using OCC.UI.Webhost.Models;

    public class AdministratorController : BaseController
    {
        //
        // GET: /Administrator/

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateSpeakersAvatars()
        {
            string localImageUrl = @"/Content/Avatar/default_user_icon.jpg";
            var defaultEvent = service.GetDefaultEvent();
            int eventid = defaultEvent.ID;
            var speakers = service.GetSpeakers(eventid);


            foreach (var speaker in speakers)
            {
                Speaker sp = new Speaker()
                              {
                                  ID = speaker.ID,
                                  Email = speaker.Email,
                                  Twitter = speaker.Twitter,
                                  ImageUrl = speaker.ImageUrl
                              };
                if (string.IsNullOrEmpty(speaker.ImageUrl)) 
                {
                    CodeCampService.Person person = service.FindPersonByEmail(sp.Email);
                    person.ImageUrl = GetImageInfo(person.Twitter, localImageUrl);
                    service.UpdatePerson(person);
                }
            }

            return RedirectToAction("Index", "Administrator");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var admins = service.GetAdministrators();

            List<Person> model = new List<Person>();
            foreach (var admin in admins)
                model.Add(new Person() 
                { 
                    ID = admin.ID, 
                    FirstName = admin.FirstName, 
                    LastName = admin.LastName 
                });

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Person admin)
        {
            var p = service.FindPersonByEmail(admin.Email);

            if (p != null)
            {
                service.AddAdministrator(p.ID);

                return RedirectToAction("Index");
            }
            else
            {
                return View(admin);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            service.RemoveAdministrator(id);

            return RedirectToAction("Index");
        }


    }
}