using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Service.Webhost.CodeCampSvc;
using CC.UI.Webhost.Models;
using MailChimp.Helper;
using MailChimp.Lists;
using MailChimp;

namespace CC.UI.Webhost.Controllers
{
    public class Subscriber
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class MyMergeVar : MergeVar
    {
        [System.Runtime.Serialization.DataMember(Name = "FNAME")]
        public string FirstName { get; set; }
        [System.Runtime.Serialization.DataMember(Name = "LNAME")]
        public string LastName { get; set; }
    }


    public class MailingListController : BaseController
    {
        public MailingListController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo) { }

        private string _mailchimpKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpAPIKey"];
        //
        // GET: /MailingList/

        public ActionResult Index()
        {
            var defaultEvent = service.GetDefaultEvent();
            int eventid = defaultEvent.ID;
            ViewBag.Event = service.GetEvent(eventid);
            int eventYear = ViewBag.Event.StartTime.Year;

            MailChimpManager mc = new MailChimpManager(_mailchimpKey);
            List<ListInfo> mailingLists = mc.GetLists().Data.Where(l=>l.Name.Contains(eventYear.ToString())).ToList();
            return View(mailingLists);
        }

        public ActionResult Update(string id)
        {
            var defaultEvent = service.GetDefaultEvent();
            int eventid = defaultEvent.ID;
            MailChimpManager mc = new MailChimpManager(_mailchimpKey);
            ListInfo mailingList = mc.GetLists().Data.Where(l=>l.Id==id).SingleOrDefault();
            List<BatchEmailParameter> batchList = new List<BatchEmailParameter>();
            if (mailingList.Name.Contains("Speakers"))
            {
                foreach (var speaker in service.GetSpeakers(eventid))
                {
                    MyMergeVar myMergeVars = new MyMergeVar();
                    myMergeVars.FirstName = speaker.FirstName;
                    myMergeVars.LastName = speaker.LastName;           //  Create the email parameter
                    BatchEmailParameter batchEmail = new BatchEmailParameter();
                    EmailParameter email = new EmailParameter()
                    {
                        Email = speaker.Email

                    };
                    batchEmail.Email = email;
                    batchEmail.MergeVars = myMergeVars;

                    batchList.Add(batchEmail);
                }
            }
            if (mailingList.Name.Contains("Attendees"))
            {
                foreach (var attendee in service.GetAllAttendees(eventid))
                {
                    MyMergeVar myMergeVars = new MyMergeVar();
                    myMergeVars.FirstName = attendee.FirstName;
                    myMergeVars.LastName = attendee.LastName;           //  Create the email parameter
                    EmailParameter email = new EmailParameter()
                    {
                        Email = attendee.Email
                    };
                    BatchEmailParameter batchEmail = new BatchEmailParameter();
                    batchEmail.Email = email;
                    batchEmail.MergeVars = myMergeVars;

                    batchList.Add(batchEmail);

                }

            }
            BatchSubscribeResult bResult = mc.BatchSubscribe(id, batchList, false, true, true);
            //MyMergeVar myMergeVars = new MyMergeVar();
            //myMergeVars.FirstName = subscriber.FirstName;
            //myMergeVars.LastName = subscriber.LastName;           //  Create the email parameter
            //EmailParameter email = new EmailParameter()
            //{
            //    Email = subscriber.EmailAddress
            //};

            //EmailParameter results = mc.Subscribe(_mailchimpGroupId, email, myMergeVars);
            

            return RedirectToAction("Index");
        }

    }
}
