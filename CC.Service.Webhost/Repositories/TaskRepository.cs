using System;
using System.Collections.Generic;
using System.Linq;
using CC.Data;
using CC.Service.Webhost.Services;
using CC.Service.Webhost.Tools;
using Person = CC.Service.Webhost.Services.Person;
using Session = CC.Service.Webhost.Services.Session;
using Task = CC.Service.Webhost.Services.Task;

namespace CC.Service.Webhost.Repositories
{
    public class TaskRepository : RepositoryBase
    {
        public TaskRepository() : base(new CCDB())
        {
        }
        public TaskRepository(CCDB dbContext)
            : base(dbContext)
        {
        }

        public IList<Person> GetTaskAssignees(int taskId)
        {
            var people = new List<Person>();

            var assignedTasks = _dbContext.PersonTasks
                .Include("Person")
                .Where(s => s.Task_ID == taskId)
                .ToList();

            people.AddRange(assignedTasks.Select(assignedTask => assignedTask.Person.Map()));
            return people;
        }

        public IList<Task> GetAllCurrentEventTasks(int eventId)
        {
            var tasks = new List<Task>();

            var currentEventTasks = _dbContext.Tasks
                .Include("PersonTasks.Person")
                .Include("Event")
                .Where(t => t.Event_ID == eventId && t.Disabled == false)
                .ToList();

            MapAllCurrentEventTasks(tasks, currentEventTasks);
            return tasks;
        }

        private static void MapAllCurrentEventTasks(ICollection<Task> tasks, IEnumerable<Data.Task> currentEventTasks)
        {
            foreach (var currentEventTask in currentEventTasks)
            {
                //var eventTask = new Task
                //{
                //    Id = currentEventTask.ID,
                //    EventID = currentEventTask.Event_ID,
                //    Capacity = currentEventTask.Capacity,
                //    Description = currentEventTask.Description,
                //    StartTime = currentEventTask.StartTime,
                //    EndTime = currentEventTask.EndTime
                //};
                var eventTask = currentEventTask.Map();


                foreach (var personTask in currentEventTask.PersonTasks)
                {
                    eventTask.Assignees.Add(personTask.Person.Map());
                }
                tasks.Add(eventTask);
            }
        }

        public IList<Task> GetTasksForAssignee(int eventId, int personId)
        {
            //using (var db = new OCCDB())
            //{
            //    var tasks = (from s in db.VolunteerTasks.Include("Volunteers")
            //                 where s.Event_ID == eventId
            //                 orderby s.Capacity
            //                 select s).ToList() as IList<Data.VolunteerTask>;

            //    return tasks.Select(task => task.Map()).ToList();
            //}
            return new List<Task>();
        }

        public void AssignVolunteerTaskToPerson(Task task, Person person)
        {
                var e = _dbContext.Tasks.Find(task.Id);

                if (e == null)
                {
                    throw new ArgumentException("Task not found");
                }

                //CC.Data.Task bcTask = e.Map();

                //task.Volunteers.Add()

                //List<Track> result = new List<Track>();
                //foreach (var track in e.Tracks)
                //    result.Add(track.Map());

                //result
        }

        public void AssignTaskToPerson(Task task)
        {
            if (task == null || task.Assignees.Count == 0)
            {
                return;
            }

            if (task.Id == 0 || task.Assignees[0].ID == 0)
            {
                return;
            }

            var personId = task.Assignees[0].ID;

            Data.Task assignedTask;
            Data.Person assignedPerson;

            var t = new Data.PersonTask {Person_ID = task.Assignees[0].ID, Task_ID = task.Id};
            _dbContext.PersonTasks.Add(t);
            _dbContext.SaveChanges();

            assignedTask = _dbContext.Tasks.FirstOrDefault(at => at.ID == task.Id);
            assignedPerson = _dbContext.People.FirstOrDefault(ap => ap.ID == personId);

            IMailService mailService = new SmtpMailService();
            mailService.SendTaskRegistrationMail(assignedTask, assignedPerson.Email);
        }

        public void RemoveTaskFromPerson(Task task)
        {
            if (task == null || task.Assignees.Count == 0)
            {
                return;
            }

            //enter email address here
            const string boardEmailAddress = "";

            int personId = task.Assignees[0].ID;
            int taskId = task.Id;
            var pt = _dbContext.PersonTasks.FirstOrDefault(x => x.Person_ID == personId && x.Task_ID == taskId);

            if (pt == null)
            {
                return;
            }
            _dbContext.PersonTasks.Remove(pt);
            _dbContext.SaveChanges();

            var unAssignedTask = _dbContext.Tasks.FirstOrDefault(t => t.ID == taskId);

            IMailService mailService = new SmtpMailService();
            mailService.SendTaskRevokeMail(unAssignedTask, boardEmailAddress);
        }

        public Task GetTaskById(int taskId)
        {
            var dcTask = new Task();

            var task = _dbContext.Tasks
                .Include("Event")
                .Where(s => s.ID == taskId)
                .SingleOrDefault();

            if (task != null)
            {
                dcTask = task.Map();
            }
            return dcTask;
        }

        public void AddTaskToEvent(Task newTask)
        {
            if (newTask == null)
            {
                throw new ArgumentNullException("newTask", "task cannot be null");
            }

            var task = newTask.Map();
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }

        public void DisableTask(int existingTaskId)
        {
            if (existingTaskId == 0)
            {
                throw new ArgumentOutOfRangeException("existingTaskId", "task Id must be within a valid range.");
            }

            var existingTask = _dbContext.Tasks.FirstOrDefault(t => t.ID == existingTaskId);
            if (existingTask != null)
            {
                existingTask.Disabled = true;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateTask(Task existingTask)
        {
            if (existingTask == null)
            {
                throw new ArgumentNullException("existingTask", "task cannot be null");
            }

            var task = _dbContext.Tasks.FirstOrDefault(t => t.ID == existingTask.Id);
            if (task != null)
            {
                task.StartTime = existingTask.StartTime;
                task.EndTime = existingTask.EndTime;
                task.Description = existingTask.Description;
                task.Capacity = existingTask.Capacity;
                task.Event_ID = existingTask.EventID;
                _dbContext.SaveChanges();
            }
        }
    }
}