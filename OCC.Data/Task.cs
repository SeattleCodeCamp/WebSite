namespace OCC.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Task
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Disabled { get; set; }

        public int Capacity { get; set; }

        [ForeignKey("Event")]
        public int Event_ID { get; set; }        
        public Event Event { get; set; }

        public virtual ICollection<PersonTask> PersonTasks { get; set; }

        public Task()
        {
            this.PersonTasks = new List<PersonTask>();
        }
    }
}