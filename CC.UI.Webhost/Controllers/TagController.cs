using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Service.Webhost.CodeCampSvc;
using CC.UI.Webhost.Models;

namespace CC.UI.Webhost.Controllers
{
    public class TagController : BaseController
    {

        public TagController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo) { }


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

            var newTag = new CC.Service.Webhost.Services.Tag()
            {
                ID = tag.Id,
                TagName = tag.TagName 
            };

            service.AddTag(newTag);

            return RedirectToAction("Index");
            
        }

    }
}
