namespace OCC.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Bio { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(100)]
        public string Blog { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string PasswordHash { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public bool IsAdmin { get; set; }

        public List<Session> Sessions { get; set; }

        public Person()
        {
            Sessions = new List<Session>();
        }
    }
}