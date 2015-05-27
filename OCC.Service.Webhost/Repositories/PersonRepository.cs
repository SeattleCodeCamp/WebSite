using System;
using System.Linq;
using OCC.Data;
using OCC.Service.Webhost.Services;
using OCC.Service.Webhost.Tools;
using Person = OCC.Service.Webhost.Services.Person;

namespace OCC.Service.Webhost.Repositories
{
    public class PersonRepository : RepositoryBase
    {
        public PersonRepository(OCCDB dbContext)
            : base(dbContext)
        {
        }

        public int RegisterPerson(Person person)
        {
            var p = new OCC.Data.Person();
            Mapper.CopyProperties(person, p);

            _dbContext.People.Add(p);
            _dbContext.SaveChanges();

            return p.ID;
        }

        public Person Login(Person person)
        {
            Person dcAttendee = default(Person);

            var bcAttendee =
                _dbContext.People
                    .SingleOrDefault(p =>
                        p.Email == person.Email &&
                        p.PasswordHash == person.PasswordHash);

            if (bcAttendee != null)
            {
                dcAttendee = new Person();
                Mapper.CopyProperties(bcAttendee, dcAttendee);
            }

            return dcAttendee;
        }

        public Person FindPersonByEmail(string email)
        {
            Person dcAttendee = default(Person);

            var bcAttendee = _dbContext.People.Where(p => p.Email == email)
                .SingleOrDefault();

            if (bcAttendee != null)
            {
                dcAttendee = new Person();
                Mapper.CopyProperties(bcAttendee, dcAttendee);
            }
            return dcAttendee;
        }

        public void ResetPassword(string emailAddress, string temporaryPassword, string temporaryPasswordHash)
        {
            if (String.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(emailAddress, "email address must be provided.");
            }

            var bcAttendee =
                _dbContext.People.SingleOrDefault(p => p.Email == emailAddress);

            if (bcAttendee == null)
            {
                throw new ArgumentOutOfRangeException(emailAddress, "attendee was not found.");
            }

            if (!String.IsNullOrEmpty(temporaryPasswordHash))
            {
                bcAttendee.PasswordHash = temporaryPasswordHash;
            }
            _dbContext.SaveChanges();

            IMailService svc = new SmtpMailService();
            svc.SendPasswordResetMail(emailAddress, temporaryPassword);
        }

        public void ChangePassword(int id, string oldPasswordHash, string newPasswordHash)
        {
            var p = _dbContext.People.Find(id);

            if (String.IsNullOrEmpty(p.PasswordHash))
            {
                throw new ArgumentNullException(oldPasswordHash);
            }
            if (p.PasswordHash == oldPasswordHash)
            {
                p.PasswordHash = newPasswordHash;
            }
            _dbContext.SaveChanges();

            IMailService svc = new SmtpMailService();
            svc.SendPasswordChangeMail(p.Email);
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
            p.Location = person.Location;
            p.TShirtSize = person.TShirtSize;

            _dbContext.SaveChanges();
        }
    }
}