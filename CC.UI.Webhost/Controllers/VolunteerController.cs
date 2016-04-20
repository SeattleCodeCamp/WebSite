using System;

namespace CC.UI.Webhost.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CC.Service.Webhost.Services;
    using Models;

    public class VolunteerController : BaseController
    {
        private const string CONST_TASK_PARAMETER_ID = "taskId";

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int eventId)
        {
            var taskList = this.service.GetAllCurrentEventTasks(eventId);
            return View(FormatCurrentEventTasks(taskList));
        }

        private IList<VolunteerTask> FormatCurrentEventTasks(ICollection<Task> taskList)
        {
            IList<VolunteerTask> model = new List<VolunteerTask>();

            if (taskList != null)
            {
                model = new List<VolunteerTask>(taskList.Count());
                foreach (var task in taskList)
                {
                    model.Add(new VolunteerTask
                    {
                        Capacity = task.Capacity,
                        Description = task.Description,
                        StartTime = task.StartTime,
                        EndTime = task.EndTime,
                        Id = task.Id
                    });
                }                
            }
            return model;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create(int eventId)
        {
            var eventForTask = service.GetEvent(eventId);
            var taskStart = DateTime.Now;
            var taskEnd = DateTime.Now;
            
            if (eventForTask != null)
            {
                taskStart = eventForTask.StartTime;
                taskEnd = eventForTask.EndTime;
            }

            VolunteerTask task = new VolunteerTask { EventId = eventId, StartTime = taskStart, EndTime = taskEnd };
            return View(task);
        } 

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(VolunteerTask newTask)
        {
            try
            {
                service.AddTaskToEvent( new Task
                                            {
                                                StartTime = newTask.StartTime,
                                                EndTime = newTask.EndTime,
                                                Capacity = newTask.Capacity,
                                                EventID = newTask.EventId,
                                                Description = newTask.Description
                                            });
                return RedirectToAction("Index");
            }
            catch
            {
                return View(newTask);
            }
        }
        
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(int id)
        {
            var model = service.GetTaskById(id);
            return View(MapModel(model));
        }

        private VolunteerTask MapModel(Task model)
        {
            var vt = new VolunteerTask
            {
                Id = model.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Description = model.Description,
                Capacity = model.Capacity,
                Volunteers = new List<Volunteer>(model.Assignees.Count()),
            };

            if (model.Event != null)
            {
                vt.Event = new Models.Event {Name = model.Event.Name, ID = model.Event.ID};
            }

            if (model.Assignees != null)
            {
                model.Assignees
                     .ToList()
                     .ForEach(a => vt.Volunteers
                                     .Add(new Volunteer
                                              {
                                                  FirstName = a.FirstName, 
                                                  LastName = a.LastName,
                                              }));   
            }
            return vt;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(VolunteerTask volunteerTask)
        {
            try
            {
                var task = service.GetTaskById(volunteerTask.Id);
                task.Name = volunteerTask.Name;
                task.Description = volunteerTask.Description;
                task.Capacity = volunteerTask.Capacity;
                task.StartTime = volunteerTask.StartTime;
                task.EndTime = volunteerTask.EndTime;
                task.EndTime = volunteerTask.EndTime;
                task.EventID = volunteerTask.EventId;
                service.UpdateTask(task);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                int taskId;
                var taskIdPassedInFromFormPost = Request[CONST_TASK_PARAMETER_ID];

                Int32.TryParse(taskIdPassedInFromFormPost, out taskId);

                if (taskId == 0)
                {
                    throw new ApplicationException("taskId is invalid.");
                }

                service.DisableTask(taskId);   
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
