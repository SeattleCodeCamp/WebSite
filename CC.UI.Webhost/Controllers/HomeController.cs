using LitJson;
using Services = CC.Service.Webhost.Services;
using CC.Service.Webhost.CodeCampSvc;

namespace CC.UI.Webhost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CC.UI.Webhost.Models;
    using System.Web.UI;
    using Tasks = System.Threading.Tasks;

    [RequireHttps]
    public class HomeController : BaseController
    {
        // Magic Strings
        private const string CONST_TASK_PARAMETER_ID = "taskId";
        private const string CONST_DEFAULT_ICON_URL = "/Content/Avatar/default_user_icon.jpg";

        //Test helper
        public HomeController(ICodeCampService service, ICodeCampServiceRepository repo)
            : base(service, repo)
        {
        }

        public async Tasks.Task<ActionResult> Index(int eventid)
        {
            var currentEvent = service.GetEvent(eventid);
            currentEvent.Name = await service.GetKey();

            ViewBag.Event = currentEvent;
            ViewBag.Message = currentEvent.Name;
            if (CurrentUser != null)
            {
                ViewBag.Rsvp = service.GetEventAttendee(eventid, CurrentUser.ID);
                ViewBag.HasSubmittedRating = service.HasSubmittedRating(CurrentUser.ID, eventid);
            }
            List<Announcement> model = new List<Announcement>();
            var announcements = service.GetCurrentAnnouncements(eventid);
            foreach (var a in announcements)
                model.Add(new Announcement { Title = a.Title, Content = a.Content });

            return View(model);
        }

        public ActionResult RsvpYes(int eventid)
        {
            if (CurrentUser != null)
                service.Rsvp(eventid, CurrentUser.ID, "YES");

            return RedirectToAction("Index");
        }

        public ActionResult RsvpNo(int eventid)
        {
            if (CurrentUser != null)
                service.Rsvp(eventid, CurrentUser.ID, "NO");

            return RedirectToAction("Index");
        }

        public ActionResult RsvpMaybe(int eventid)
        {
            if (CurrentUser != null)
                service.Rsvp(eventid, CurrentUser.ID, "MAYBE");

            return RedirectToAction("Index");
        }

        public ActionResult RsvpChange(int eventid)
        {
            if (CurrentUser != null)
                service.Rsvp(eventid, CurrentUser.ID, "");

            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public ActionResult Speakers(int? id)
        {
            var defaultEvent = service.GetDefaultEvent();
            int eventid = defaultEvent.ID;
            ViewBag.Event = service.GetEvent(eventid);

            var speakers = service.GetSpeakers(eventid);

            var model = new List<Speaker>();

            foreach (var speaker in speakers)
            {
                Speaker sp = new Speaker()
                {
                    ID = speaker.ID,
                    FirstName = speaker.FirstName,
                    LastName = speaker.LastName,
                    ImageUrl = speaker.ImageUrl
                };
                List<Session> sessions = new List<Session>();
                foreach (var session in service.GetSpeakerSessions(eventid, sp.ID))
                {
                    sessions.Add(new Session()
                    {
                        ID = session.ID,
                        Name = session.Name
                    });
                }
                sp.Sessions = sessions;
                model.Add(sp);

            }
            return View(model);
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public ActionResult Sessions(int? id)
        {
            var defaultEvent = service.GetDefaultEvent();
            int eventid = defaultEvent.ID;
            ViewBag.Event = service.GetEvent(eventid);

            var sessions = service.GetSessions(eventid);
            var timeSlots = service.GetTimeslots(eventid);

            var viewModel = new SessionsViewModel();

            foreach (var session in sessions)
                if ((id == null) || (id == -1) || (session.TimeslotID == id))
                {
                    viewModel.Sessions.Add(new Session()
                    {
                        ID = session.ID,
                        Name = session.Name,
                        Description = session.Description,
                        Speaker = session.Speaker,
                        ImageUrl = session.ImageUrl,
                        SpeakerID = session.SpeakerID,
                        StartTime = session.StartTime.HasValue ? session.StartTime.Value.ToString() : string.Empty,
                        EndTime = session.EndTime.HasValue ? session.EndTime.Value.ToString() : string.Empty,
                        Status = session.Status,
                        Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                    });
                }

            foreach (var time in timeSlots)
            {
                viewModel.Timeslots.Add(new Timeslot()
                {
                    ID = time.ID,
                    EventID = time.EventID,
                    StartTime = time.StartTime,
                    EndTime = time.EndTime,
                    Name = time.Name
                });
            }

            return View(viewModel);
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Client, VaryByParam = "eventid")]
        public ActionResult Agenda(int eventid)
        {
            var tracks = service.GetAgenda(eventid);

            List<Track> model = new List<Track>();

            foreach (var track in tracks)
            {
                Track t = new Track() { ID = track.ID, Name = track.Name, Description = track.Description };

                foreach (var session in track.Sessions.OrderBy((s) => s.StartTime))
                    t.Sessions.Add(new Session()
                    {
                        ID = session.ID,
                        Name = session.Name,
                        Description = session.Description,
                        Speaker = session.Speaker,
                        SpeakerID = session.SpeakerID,
                        StartTime = session.StartTime.Value.ToShortTimeString(),
                        EndTime = session.EndTime.Value.ToShortTimeString(),
                        Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                    });

                model.Add(t);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Rate(int eventid)
        {
            //int currentId = CurrentUser.ID;
            //var agenda = service.GetMyAgenda(eventid, currentId);// CurrentUser.ID);
            var sessions = service.GetSessions(eventid);
            var timeSlots = service.GetTimeslots(eventid);
            //ViewBag.Agenda = agenda;
            ViewBag.Sessions = sessions;
            ViewBag.TimeSlots = timeSlots;
            Rate model = new Rate();
            //List<int> timeSlotIDs = new List<int> { 22, 23, 24, 26, 27, 28, 29 };
            foreach (var timeSlot in timeSlots)
            {
                RateSession rs = new RateSession
                {
                    TimeSlotID = timeSlot.ID
                };
                model.RateSessions.Add(rs);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Rate(int eventid, FormCollection frm)
        {
            var rating = new Services.Rate();
            rating.UserID = CurrentUser.ID;
            rating.EventID = eventid;
            rating.RateSignin = int.Parse(frm["RateSignin"]);
            rating.RateSwag = int.Parse(frm["RateSwag"]);
            rating.RateFood = int.Parse(frm["RateFood"]);
            rating.ReferralSource = int.Parse(frm["RefSource"]);
            rating.Comments = frm["Comments"];

            var rateSessions = new List<Services.RateSession>();
            for (int i = 0; i < 7; i++)
            {
                if (frm[string.Format("SessionID_{0}", i)] == null)
                    continue;

                int sessionID = int.Parse(frm[string.Format("SessionID_{0}", i)]);

                int timeslotID = int.Parse(frm[string.Format("Timeslot_{0}", i)]);
                int rankSession = int.Parse(frm[string.Format("RateSession_{0}", i)]);
                string comments = frm[string.Format("Comments_{0}", i)];
                if (rankSession > 0)
                {
                    var rateSession = new Services.RateSession();
                    rateSession.Rating = rankSession;
                    rateSession.SessionID = sessionID;
                    rateSession.TimeSlotID = timeslotID;
                    rateSession.Comments = comments;
                    rateSessions.Add(rateSession);
                }
            }
            rating.RatedSessions = rateSessions.ToList();
            service.CreateRateSession(rating);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult MyAgenda(int eventid)
        {
            var agenda = service.GetMyAgenda(eventid, CurrentUser.ID);
            var currentEvent = service.GetEvent(eventid);
            ViewBag.EventStarted = (currentEvent.EndTime.AddDays(-1) < DateTime.Now);
            List<Session> model = new List<Session>();

            foreach (var session in agenda)
                model.Add(new Session()
                {
                    ID = session.ID,
                    Name = session.Name,
                    Description = session.Description,
                    Speaker = session.Speaker,
                    SpeakerID = session.SpeakerID,
                    StartTime = session.StartTime.Value.ToShortTimeString(),
                    EndTime = session.EndTime.Value.ToShortTimeString(),
                    Location = string.IsNullOrEmpty(session.Location) ? string.Empty : session.Location
                });

            return View(model);
        }

        //[HttpPost]
        [Authorize]
        public ActionResult RemoveFromMyAgenda(int id)
        {
            service.DeleteMyAgendaItem(id, CurrentUser.ID);
            return RedirectToAction("MyAgenda", "Home");
        }

        public ActionResult Sponsors(int eventid)
        {
            ViewBag.Event = service.GetEvent(eventid);

            var sponsors = service.GetSponsors(eventid);

            List<Sponsor> model = new List<Sponsor>();

            foreach (var sponsor in sponsors)
                model.Add(new Sponsor()
                {
                    ID = sponsor.ID,
                    Name = sponsor.Name,
                    Description = sponsor.Description,
                    SponsorshipLevel = sponsor.SponsorshipLevel,
                    WebsiteUrl = sponsor.WebsiteUrl,
                    Logo = sponsor.Image == null ? null : new Infrastructure.WebImageOCC(sponsor.Image)
                });

            return View(model);
        }

        public ActionResult FAQ()
        {
            return View();
        }

        /// <summary>
        /// This is the default view for the Volunteers View accordion control
        /// </summary>
        /// <param name="eventid">eventId</param>
        /// <returns>/Views/Home/Volunteers.cshtml</returns>
        public ActionResult Volunteers(int eventid)
        {
            var eventTasks = service.GetAllCurrentEventTasks(eventid);

            List<VolunteerTask> model;

            if (eventTasks.Any() && eventTasks[0].Event.IsVolunteerRegistrationOpen)
            {
                model = FormatEventTasks(eventTasks).ToList();
            }
            else
            {
                model = new List<VolunteerTask>();
            }

            return View(model);
        }

        /// <summary>
        /// This method is a collaborator to the (un)register buttons on the form
        /// and allows folks to signup for tasks.
        /// </summary>
        /// <param name="id">Nullable value; used this form b/c the framework is actually looking for a non-null value type.</param>
        /// <param name="btnSubmit">This param tells the form which button was clicked</param>
        /// <param name="collection">Just in case...</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Volunteers(int? id, string btnSubmit, FormCollection collection)
        {
            int taskId;
            var taskIdPassedInFromFormPost = Request[CONST_TASK_PARAMETER_ID];

            Int32.TryParse(taskIdPassedInFromFormPost, out taskId);

            if (taskId == 0)
            {
                throw new ApplicationException("taskId is invalid.");
            }

            var currentUser = CurrentUser;
            if (currentUser != null)
            {
                var task = new Services.Task { Id = taskId };
                if (task.Assignees == null || task.Assignees.Count == 0)
                {
                    task.Assignees = new Services.Person[1];
                }
                task.Assignees[0] = new Services.Person { ID = currentUser.ID };

                switch (btnSubmit)
                {
                    case "add":
                        this.service.AssignTaskToPerson(task);
                        break;
                    case "remove":
                        this.service.RemoveTaskFromPerson(task);
                        break;
                }
                // TODO Tell the user it was successful.
                // https://github.com/akquinet/jquery-toastmessage-plugin/wiki
            }
            return RedirectToAction("Volunteers", "Home");
        }

        public ActionResult Venue(int eventid)
        {
            var event_ = service.GetEvent(eventid);

            Event model = new Event()
            {
                ID = event_.ID,
                Name = event_.Name,
                Description = event_.Description,
                TwitterHashTag = event_.TwitterHashTag,
                StartTime = event_.StartTime,
                EndTime = event_.EndTime,
                Location = event_.Location,
                Address1 = event_.Address1,
                Address2 = event_.Address2,
                City = event_.City,
                State = event_.State,
                Zip = event_.Zip
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        private IEnumerable<VolunteerTask> FormatEventTasks(ICollection<Services.Task> eventTasks)
        {
            var tasks = new List<VolunteerTask>(eventTasks.Count);

            VolunteerTask vt;
            foreach (var eventTask in eventTasks)
            {
                vt = new VolunteerTask
                {
                    Id = eventTask.Id,
                    Description = eventTask.Description,
                    StartTime = eventTask.StartTime,
                    EndTime = eventTask.EndTime,
                    Capacity = eventTask.Capacity,
                    Volunteers = new List<Volunteer>()
                };

                tasks.Add(vt);

                Volunteer v;
                foreach (var assignee in eventTask.Assignees)
                {
                    v = new Volunteer
                    {
                        FirstName = assignee.FirstName,
                        LastName = assignee.LastName,
                        Email = assignee.Email,
                        ID = assignee.ID,
                        ImageUrl =
                                    (String.IsNullOrEmpty(assignee.ImageUrl)
                                         ? CONST_DEFAULT_ICON_URL
                                         : assignee.ImageUrl)
                    };
                    vt.Volunteers.Add(v);
                }
            }
            return tasks;
        }

    }
}