namespace OCC.Data
{
    using System.ComponentModel.DataAnnotations;

    public class PersonTask
    {
        public int ID { get; set; }

        [ForeignKey("Task")]
        public int Task_ID { get; set; }

        [ForeignKey("Person")]
        public int Person_ID { get; set; }

        public Task Task { get; set; }

        public Person Person { get; set; }
    }
}