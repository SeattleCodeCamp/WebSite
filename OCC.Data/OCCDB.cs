using System;
using System.Data.Common;

namespace OCC.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class OCCDB : DbContext
    {
        public OCCDB()
            : base("OCC2012")
        {

        }

        protected OCCDB(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Sponsor> Sponsors { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Timeslot> Timeslots { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<EventAttendee> EventAttendees { get; set; }

        public DbSet<EventAttendeeRating> EventAttendeeRatings { get; set; }

        public DbSet<EventAttendeeSessionRating> EventAttendeeSessionRatings { get; set; }

        public DbSet<SessionAttendee> SessionAttendees { get; set; }

        public DbSet<PersonTask> PersonTasks { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<KeyValuePair> KeyValuePairs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Announcement>()
            //    .HasRequired(a => a.Event)
            //    .WithMany(e => e.Announcements)
            //    .HasForeignKey(a => a.FK_Event);

            //DEV ONLY !!! 
            //Database.SetInitializer<OCCDB>(new OCCDBInitializer());
        }
    }

    public class OCCDBInitializer : DropCreateDatabaseIfModelChanges<OCCDB>
    {
        protected override void Seed(OCCDB context)
        {
            Person p1 = new Person()
            {
                ID = 1,
                FirstName = "NETDA",
                LastName = "Admin",
                Email = "core@dotnetda.org"
            };

            Person p2 = new Person()
            {
                ID = 2,
                FirstName = "John",
                LastName = "Smith",
                Email = "john@dotnetda.org"
            };

            Person p3 = new Person()
            {
                ID = 3,
                FirstName = "Brian",
                LastName = "Hall",
                Email = "brian@dotnetda.org"
            };

            Person p4 = new Person()
            {
                ID = 4,
                FirstName = "Zdravko",
                LastName = "Danev",
                Email = "z@onetug.org"
            };

            Person p5 = new Person()
            {
                ID = 5,
                FirstName = "Esteban",
                LastName = "Garcia",
                Email = "esteban@dotnetda.org"
            };

            Event occ2011 = new Event()
            {
                ID = 1,
                Name = "Seattle Code Camp 2011",
                StartTime = new DateTime(2011, 03, 21),
                EndTime = new DateTime(2011, 02, 21),
                Address1 = "Seminole State College",
                Address2 = "100 College Dr",
                City = "Sanford",
                State = "FL",
                Zip = "32746"
            };

            Event occ2012 = new Event()
            {
                ID = 2,
                Name = "Seattle Code Camp 2012",
                StartTime = new DateTime(2012, 03, 25),
                EndTime = new DateTime(2012, 03, 25),
                Address1 = "Seminole State College",
                Address2 = "100 College Str",
                City = "Sanford",
                State = "FL",
                Zip = "32746"
            };

            occ2011.Announcements.Add(new Announcement() { ID = 1, Title = "call for speakers", Content = "This is the first announcement.", PublishDate = new DateTime(2012, 1, 1) });
            occ2011.Announcements.Add(new Announcement() { ID = 2, Title = "call for volunteers", Content = "This is the second announcement.", PublishDate = new DateTime(2012, 2, 1) });
            occ2011.Announcements.Add(new Announcement() { ID = 3, Title = "call for attendees", Content = "This is the third announcement.", PublishDate = new DateTime(2012, 3, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 4, Title = "call for speakers", Content = "This is the first announcement.", PublishDate = new DateTime(2012, 1, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 5, Title = "call for volunteers", Content = "This is the second announcement.", PublishDate = new DateTime(2012, 2, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 6, Title = "call for attendees", Content = "This is the third announcement.", PublishDate = new DateTime(2012, 3, 1) });

            Track t1 = new Track { ID = 11, Name = "Windows Phone 7", Description = "Windows Phone 7 development" };
            Track t2 = new Track { ID = 12, Name = "Windows 8", Description = "Windows 8 development" };
            Track t3 = new Track { ID = 13, Name = "Architecture", Description = "Architecture, P and P" };

            t1.Sessions.Add(new Session() { ID = 1, Event_ID = 2, Name = "Silverlight for WP7", Description = "Introduction in Silverlight programming with windows phone 7", Speaker = p2 });
            t1.Sessions.Add(new Session() { ID = 2, Event_ID = 2, Name = "XNA for WP7", Description = "Introduction in XNA programming with windows phone 7", Speaker = p3 });
            t2.Sessions.Add(new Session() { ID = 3, Event_ID = 2, Name = "Intro in Win 8", Description = "Introduction in Windows 8", Speaker = p4 });
            t3.Sessions.Add(new Session() { ID = 4, Event_ID = 2, Name = "P & P", Description = "Patterns and practices", Speaker = p5 });

            occ2012.Tracks.Add(t1);
            occ2012.Tracks.Add(t2);
            occ2012.Tracks.Add(t3);

            context.People.Add(p1);
            context.People.Add(p2);
            context.People.Add(p3);
            context.People.Add(p4);
            context.People.Add(p5);

            context.Events.Add(occ2011);
            context.Events.Add(occ2012);

            context.KeyValuePairs.Add(new KeyValuePair()
            {
                Id = "tshirtSizes",
                Value = "[{\"Item1\":1,\"Item2\":\"one\"},{\"Item1\":2,\"Item2\":\"two\"}]"
            });

            context.SaveChanges();
        }
    }
}