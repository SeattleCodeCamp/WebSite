using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OCC.UI.Webhost.Models;

namespace OCC.UI.Webhost.Controllers
{
    public class TagController : BaseController
    {

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var tagCollection = service.GetTags();
            var model = tagCollection.Select(tag => new Tag()
            {
                Id = tag.ID, 
                SessionsCount = tag.SessionsCount, 
                TagName = tag.TagName

            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int tagId)
        {
            var result = service.DeleteTag(tagId);
            
            return RedirectToAction("Index");
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Tag tag)
        {

            var newTag = new CodeCampService.Tag()
            {
                ID = tag.Id,
                TagName = tag.TagName 
            };

            service.AddTag(newTag);

            return RedirectToAction("Index");
            
        }

    }
}
