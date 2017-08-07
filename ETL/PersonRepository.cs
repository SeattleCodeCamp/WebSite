using System;
using System.Linq;
using CC.Data;
using System.Collections.Generic;
using CC.Service.Webhost.Repositories;

namespace ETL
{
    public class PersonRepository : RepositoryBase
    {
        public PersonRepository() : base(new CCDB())
        {
        }
        public PersonRepository(CCDB dbContext)
            : base(dbContext)
        {
        }

        public int RegisterPerson(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();

            return person.ID;
        }

        public IEnumerable<Person> FindAllPersons()
        {
            //Person dcAttendee = default(Person);

            return _dbContext.People;

            //if (bcAttendee != null)
            //{
            //    dcAttendee = new Person();
            //    Mapper.CopyProperties(bcAttendee, dcAttendee);
            //}
            //return dcAttendee;
        }

        public IList<Person> FindPerson(string firstName, string lastName)
        {
            return _dbContext.People.Where(p => p.FirstName == firstName && p.LastName == lastName).ToList();
        }

        public IList<Person> FindPerson(int id)
        {
            return _dbContext.People.Where(p => p.ID == id).ToList();
        }

        public void UpdatePerson(Person person)
        {
            var p = _dbContext.People.Find(person.ID);

            p.FirstName = person.FirstName;
            p.LastName = person.LastName;
            p.Title = person.Title;
            p.Bio = person.Bio;
            p.Website = person.Website;
            p.Blog = person.Blog;
            p.Twitter = person.Twitter;
            p.ImageUrl = person.ImageUrl;
            //p.Image = person.Image;
            p.Location = person.Location;
            p.TShirtSize = person.TShirtSize;
            p.LoginProvider = person.LoginProvider;
            p.Sessions = person.Sessions;

            _dbContext.SaveChanges();
        }
    }
}