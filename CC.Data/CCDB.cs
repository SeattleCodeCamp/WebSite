using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CC.Data
{
    
    public class CCDB : DbContext
    {
        public CCDB()
            : base("DefaultConnection")
        {
            //Database.SetInitializer<CCDB>(new DropCreateDatabaseIfModelChanges<CCDB>());
            Database.SetInitializer<CCDB>(null);
        }

        protected CCDB(DbConnection existingConnection, bool contextOwnsConnection)
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
        }
    }

    public class OCCDBInitializer : DropCreateDatabaseIfModelChanges<CCDB>
    {
        protected override void Seed(CCDB context)
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
                Name = "Seattle Code Camp 2016",
                StartTime = new DateTime(2016, 09, 10, 08, 00, 00),
                EndTime = new DateTime(2016, 09, 10, 17, 00, 00),
                Location = "Seattle University",
                Address1 = "901 12th Avenue",
                Address2 = "Pigott Building",
                City = "Seattle",
                State = "WA",
                Zip = "98122",
                IsDefault = true
            };

            occ2011.Announcements.Add(new Announcement() { ID = 1, Title = "call for speakers", Content = "This is the first announcement.", PublishDate = new DateTime(2012, 1, 1) });
            occ2011.Announcements.Add(new Announcement() { ID = 2, Title = "call for volunteers", Content = "This is the second announcement.", PublishDate = new DateTime(2012, 2, 1) });
            occ2011.Announcements.Add(new Announcement() { ID = 3, Title = "call for attendees", Content = "This is the third announcement.", PublishDate = new DateTime(2012, 3, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 4, Title = "call for speakers", Content = "This is the first announcement.", PublishDate = new DateTime(2012, 1, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 5, Title = "call for volunteers", Content = "This is the second announcement.", PublishDate = new DateTime(2012, 2, 1) });
            occ2012.Announcements.Add(new Announcement() { ID = 6, Title = "call for attendees", Content = "This is the third announcement.", PublishDate = new DateTime(2012, 3, 1) });

            Track t1 = new Track { ID = 11, Name = "Windows Phone 7", Description = "Windows Phone 7 development" };
            Track t2 = new Track { ID = 12, Name = "Windows 10", Description = "Windows 10 development" };
            Track t3 = new Track { ID = 13, Name = "Architecture", Description = "Architecture, P and P" };

            Timeslot tslot1 = new Timeslot { ID = 1, Event_ID = 2, StartTime = occ2012.StartTime, EndTime = occ2012.StartTime.AddMinutes(50) };
            Timeslot tslot2 = new Timeslot { ID = 2, Event_ID = 2, StartTime = occ2012.StartTime.AddHours(1), EndTime = occ2012.StartTime.AddHours(1).AddMinutes(50) };
            Timeslot tslot3 = new Timeslot { ID = 3, Event_ID = 2, StartTime = occ2012.StartTime.AddHours(2), EndTime = occ2012.StartTime.AddHours(2).AddMinutes(50) };
            Timeslot tslot4 = new Timeslot { ID = 4, Event_ID = 2, StartTime = occ2012.StartTime.AddHours(3), EndTime = occ2012.StartTime.AddHours(3).AddMinutes(50) };

            t1.Sessions.Add(new Session() { ID = 1, Event_ID = 2, Timeslot_ID = 3, Name = "Silverlight for WP7", Description = "Introduction to Silverlight programming with windows phone 7", Speaker = p2, Status= "APPROVED" });
            t1.Sessions.Add(new Session() { ID = 2, Event_ID = 2, Timeslot_ID = 1, Name = "XNA for WP7", Description = "Introduction to XNA programming with windows phone 7", Speaker = p3, Status = "APPROVED" });
            t2.Sessions.Add(new Session() { ID = 3, Event_ID = 2, Timeslot_ID = 2, Name = "Intro to Windows 10", Description = "Introduction to Windows 10", Speaker = p4, Status = "APPROVED" });
            t3.Sessions.Add(new Session() { ID = 4, Event_ID = 2, Timeslot_ID = 4, Name = "Patterns and Practices", Description = "Proven practices for predictable results", Speaker = p5, Status = "APPROVED" });

            occ2012.Tracks.Add(t1);
            occ2012.Tracks.Add(t2);
            occ2012.Tracks.Add(t3);

            occ2012.Timeslots.Add(tslot1);

            context.People.Add(p1);
            context.People.Add(p2);
            context.People.Add(p3);
            context.People.Add(p4);
            context.People.Add(p5);

            context.Events.Add(occ2011);
            context.Events.Add(occ2012);

            context.Tags.Add(new Tag { TagName = "Architecture"});
            context.Tags.Add(new Tag { TagName = "Career" });
            context.Tags.Add(new Tag { TagName = "Cloud" });
            context.Tags.Add(new Tag { TagName = "Data" });
            context.Tags.Add(new Tag { TagName = "Game/VR" });
            context.Tags.Add(new Tag { TagName = "Mobile" });
            context.Tags.Add(new Tag { TagName = "Testing" });
            context.Tags.Add(new Tag { TagName = "User Experience" });
            context.Tags.Add(new Tag { TagName = "Web" });
            context.Tags.Add(new Tag { TagName = "Other" });
            context.Tags.Add(new Tag { TagName = "IoT" });
            context.Tags.Add(new Tag { TagName = "Hardware" });

            context.KeyValuePairs.Add(new KeyValuePair()
            {
                Id = "tshirtSizes",
                Value = "[{\"Item1\":1,\"Item2\":\"Don't want one\"},{\"Item1\":2,\"Item2\":\"Small\"},{\"Item1\":2,\"Item2\":\"Medium\"},{\"Item1\":2,\"Item2\":\"Large\"},{\"Item1\":2,\"Item2\":\"X-Large\"},{\"Item1\":2,\"Item2\":\"XX-Large\"}]"
            });

            context.SaveChanges();
        }
    }
}